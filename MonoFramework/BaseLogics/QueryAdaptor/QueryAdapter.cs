using DbPooling;
using MonoFrame.QueryGenerator;
using MonoFrame.QueryGenerator.Compilers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MonoFrame.BaseLogics
{
    public static  class QueryAdapter
    {

        public static string DataBaseType;
        static QueryAdapter()
        {
            var builder = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            var AppSettings = configuration.GetSection("AppSettings");
            DataBaseType = (AppSettings.GetSection("DataBaseType").Value);
        }
       public static Query GetSql(string table)
        {

            Query query =  new Query(table);
            return query;
        }

        public static Query GetSql()
        {
            Query query = new Query();    
            return query;
        }


        public static SqlResult Compile(this Query query)
        {

            var compiler = new SqlServerCompiler();
            return compiler.Compile(query);
        }

        public static SqlResult Compile(this Query query, string databaseType)
        {
            Compiler compiler = null;
            if (DataBaseType == ConnectionType.MYSQL)
            {
                compiler = new MySqlCompiler();
            }
            else if (DataBaseType == ConnectionType.SQL)
            {
                compiler = new MySqlCompiler();
            }
            else if (DataBaseType == ConnectionType.ORACLE)
            {
                compiler = new OracleCompiler();
            }

            else if (DataBaseType == ConnectionType.SQLITE)
            {
                compiler = new SqliteCompiler();
            }
           
            else if (DataBaseType == ConnectionType.POSTRGRESQL)
            {
                compiler = new PostgresCompiler();
            }

           
            return compiler.Compile(query);
        }
    }

}
