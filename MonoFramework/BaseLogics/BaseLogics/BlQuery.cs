using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using MonoFrame.BaseObject;
using MonoFrame.QueryGenerator;

namespace MonoFrame.BaseLogics
{
    internal static class BlQuery
    {
        internal static SqlResult GetInsertQuery(DoBase aDoBase)
        {

          return  QueryAdapter.GetSql(aDoBase.GetTableName()).AsInsert(aDoBase, returnId: true).Compile();
        }

        internal static SqlResult GetUpdateQuery(DoBase aDoBase, object ChangedValues)
        {
           
            return QueryAdapter.GetSql(aDoBase.GetTableName()).AsUpdate(ChangedValues).Compile();
        }


        internal static SqlResult GetDeleteQuery(DoBase aDoBase)
        {

            return QueryAdapter.GetSql(aDoBase.GetTableName()).Where(aDoBase.GetPrimaryKey(), aDoBase.GetPrimaryKeyValue()).AsDelete().Compile();
        }


        internal static SqlResult GetOpenQuery(DoBase aDoBase)
        {

            return QueryAdapter.GetSql(aDoBase.GetTableName()).Where(aDoBase.GetPrimaryKey(), aDoBase.GetPrimaryKeyValue()).Compile();
        }

    }
}
