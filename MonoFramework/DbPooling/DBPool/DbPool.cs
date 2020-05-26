using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OracleClient;
using System.Data.SqlClient;
using Npgsql; 
using System.Text;
using System.Threading;
using MonoFrame.DataBase;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DbPooling
{
   public static class DbPool 
    {
        
        private static int POOL_SIZE ;
        private static  int MAX_IDLE_TIME;
       private static readonly object lockobj = new object();
        private static readonly string ConnectionString;
        private static readonly string DataBaseType ;
        static DbPool()
        {
           

            var builder = new ConfigurationBuilder()
         .SetBasePath(Directory.GetCurrentDirectory())
         .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            var AppSettings = configuration.GetSection("AppSettings");
            POOL_SIZE = Convert.ToInt32(AppSettings.GetSection("PoolSize").Value);
            MAX_IDLE_TIME = Convert.ToInt32(AppSettings.GetSection("Max Idle Time").Value);
            ConnectionString = (AppSettings.GetSection("Connection String").Value);
            DataBaseType = (AppSettings.GetSection("DataBaseType").Value);

            Connections = new IDbConnection[POOL_SIZE];
            Locks = new int[POOL_SIZE];
            Dates = new DateTime[POOL_SIZE];
        }
        private static IDbConnection[] Connections;
        private static int[] Locks;

        private static DateTime[] Dates;
        public static IDbConnection GetConnection(out int? identifier,out int valid)
        {
            for (int i = 0; i < POOL_SIZE; i++)
            {
                if (Interlocked.CompareExchange(ref Locks[i], 1, 0) == 0)
                {
                    if (Dates[i] != DateTime.MinValue && (DateTime.Now - Dates[i]).TotalMinutes > MAX_IDLE_TIME)
                    {
                        Connections[i].Dispose();
                        Connections[i] = null;
                    }

                    if (Connections[i] == null)
                    {

                       
                        lock (lockobj)
                        {
                            IDbConnection conn = CreateConnection();
                            Connections[i] = conn;
                            conn.Open();
                        }
                    }

                    Dates[i] = DateTime.Now;
                    identifier = i;
                    valid = 1;
                    return Connections[i];
                }
            }

            throw new Exception("No free connections");
        }


        private static IDbConnection CreateConnection()
        {
           
            IDbConnection returnValue = null;
            
               
                if (DataBaseType == ConnectionType.MYSQL)
                {
                    returnValue = new MySqlConnection(ConnectionString);
                }
                else if (DataBaseType == ConnectionType.SQL)
                {
                    returnValue = new SqlConnection(ConnectionString);
                }
                else if (DataBaseType == ConnectionType.ORACLE)
                {
                returnValue = new OracleConnection(ConnectionString);
                }

                else if (DataBaseType == ConnectionType.SQLITE)
                {
                    returnValue = new SqliteConnection(ConnectionString);
                }

                 else if (DataBaseType == ConnectionType.POSTRGRESQL)
                {
                returnValue = new NpgsqlConnection(ConnectionString);
                }



            return returnValue;
        }


        public static void FreeConnection(int identifier, out int valid)
        {

            //There is no need to lock since interlocked.exchange has acid transaction for multiple thread
        //    lock (lockobj)
         //   {

                valid = 0;
                if (identifier < 0 || identifier >= POOL_SIZE)
                    return;

                Interlocked.Exchange(ref Locks[identifier], 0);

              //  Connections[identifier].Close();
           // }
               
            
        }


    }
}
