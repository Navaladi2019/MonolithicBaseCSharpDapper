using System;

using DbPooling;
using MonoFrame.BaseLogics;
using MonoFrame.QueryGenerator;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            SqlResult sqlResult = QueryAdapter.GetSql("Audits").Where("TableName", "Test").Compile();

            var a = MonoFrame.DataBase.DataAccess.Query(sqlResult);

            Console.ReadLine();

        }
    }
}
