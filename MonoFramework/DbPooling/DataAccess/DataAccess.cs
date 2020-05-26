using Dapper;
using MonoFrame.QueryGenerator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using static Dapper.SqlMapper;

namespace MonoFrame.DataBase
{
    public static class DataAccess
    {


       public static int Execute(SqlResult SqlResult)
        {
            int result = 0;
            try
            {
                result = UnitOfWork.uow.GetDbconnection().Execute(SqlResult.Sql, SqlResult.NamedBindings, UnitOfWork.uow.transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                UnitOfWork.uow.returnConnectionToPoolInternal();
            }

            return result;
        }

        public static int ExecuteSP(SqlResult SqlResult)
        {
            int result = 0;
            try
            {

                // Command Type For Stored Procedure is 4
                result = UnitOfWork.uow.GetDbconnection().Execute(SqlResult.Sql, SqlResult.NamedBindings, UnitOfWork.uow.transaction,4);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                UnitOfWork.uow.returnConnectionToPoolInternal();
            }

            return result;
        }

        public static object ExecuteScalar(SqlResult SqlResult)
        {
            object result = null;
            try
            {


                // Command Type For Stored Procedure is 4
                result = UnitOfWork.uow.GetDbconnection().ExecuteScalar(SqlResult.Sql, SqlResult.NamedBindings, UnitOfWork.uow.transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                UnitOfWork.uow.returnConnectionToPoolInternal();
            }

            return result;
        }

        public static T ExecuteScalar<T>(SqlResult SqlResult)
        {
            T result = default(T);
            try
            {


                // Command Type For Stored Procedure is 4
                result = UnitOfWork.uow.GetDbconnection().ExecuteScalar<T>(SqlResult.Sql, SqlResult.NamedBindings, UnitOfWork.uow.transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                UnitOfWork.uow.returnConnectionToPoolInternal();
            }

            return result;
        }

        public static IEnumerable<dynamic> Query(SqlResult SqlResult)
        {
            IEnumerable<dynamic> result = null;
            try
            {

                result = UnitOfWork.uow.GetDbconnection().Query(SqlResult.Sql, SqlResult.NamedBindings);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                UnitOfWork.uow.returnConnectionToPoolInternal();
            }

            return result;
        }


        public static IEnumerable<TReturn> Query<TReturn>(SqlResult SqlResult, Type[] types, Func<object[], TReturn> map, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id")
        {
            IEnumerable<TReturn> result = null;
            try
            {

                result = UnitOfWork.uow.GetDbconnection().Query<TReturn>(SqlResult.Sql, types, map, SqlResult.NamedBindings, UnitOfWork.uow.transaction, buffered, splitOn);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                UnitOfWork.uow.returnConnectionToPoolInternal();
            }

            return result;

        }

        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(SqlResult SqlResult, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            IEnumerable<TReturn> result = null;
            try
            {

                result = UnitOfWork.uow.GetDbconnection().Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(SqlResult.Sql, map, SqlResult.NamedBindings, UnitOfWork.uow.transaction, buffered, splitOn, commandTimeout, commandType);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                UnitOfWork.uow.returnConnectionToPoolInternal();
            }

            return result;
        }

        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(SqlResult SqlResult, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            IEnumerable<TReturn> result = null;
            try
            {

                result = UnitOfWork.uow.GetDbconnection().Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(SqlResult.Sql, map,  param, UnitOfWork.uow.transaction,buffered,splitOn,commandTimeout, commandType);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                UnitOfWork.uow.returnConnectionToPoolInternal();
            }

            return result;
        }

        public static IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(SqlResult SqlResult, Func<TFirst, TSecond, TReturn> map, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            IEnumerable<TReturn> result = null;
            try
            {

                result = UnitOfWork.uow.GetDbconnection().Query<TFirst, TSecond, TReturn>(SqlResult.Sql, map, SqlResult.NamedBindings, UnitOfWork.uow.transaction, buffered, splitOn, commandTimeout, commandType);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                UnitOfWork.uow.returnConnectionToPoolInternal();
            }

            return result;
        }


        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(SqlResult SqlResult, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            IEnumerable<TReturn> result = null;
            try
            {

                result = UnitOfWork.uow.GetDbconnection().Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(SqlResult.Sql, map, SqlResult.NamedBindings, UnitOfWork.uow.transaction, buffered, splitOn, commandTimeout, commandType);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                UnitOfWork.uow.returnConnectionToPoolInternal();
            }

            return result;
        }

        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(SqlResult SqlResult, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            IEnumerable<TReturn> result = null;
            try
            {

                result = UnitOfWork.uow.GetDbconnection().Query<TFirst, TSecond, TThird, TFourth, TReturn>(SqlResult.Sql, map, SqlResult.NamedBindings, UnitOfWork.uow.transaction, buffered, splitOn, commandTimeout, commandType);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                UnitOfWork.uow.returnConnectionToPoolInternal();
            }

            return result;
        }




        public static IEnumerable<dynamic> Query(SqlResult SqlResult, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            IEnumerable<dynamic> result;
            try
            {

                result = UnitOfWork.uow.GetDbconnection().Query(SqlResult.Sql, SqlResult.NamedBindings, UnitOfWork.uow.transaction, buffered, commandTimeout, commandType);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                UnitOfWork.uow.returnConnectionToPoolInternal();
            }

            return result;
        }

        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(SqlResult SqlResult, Func<TFirst, TSecond, TThird, TReturn> map, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            IEnumerable<TReturn> result = null;
            try
            {

                result = UnitOfWork.uow.GetDbconnection().Query<TFirst, TSecond, TThird, TReturn>(SqlResult.Sql, map, SqlResult.NamedBindings, UnitOfWork.uow.transaction, buffered, splitOn, commandTimeout, commandType);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                UnitOfWork.uow.returnConnectionToPoolInternal();
            }

            return result;
        }
     //public static object QueryFirst(SqlResult SqlResult,Type type)
     //   {
     //     object result = null;
     //       try
     //       {

     //           result = UnitOfWork.uow.GetDbconnection().QueryFirst(type, SqlResult.Sql,SqlResult.NamedBindings,UnitOfWork.uow.transaction);

     //       }
     //       catch (Exception ex)
     //       {
     //           throw ex;
     //       }
     //       finally
     //       {
     //           UnitOfWork.uow.returnConnectionToPoolInternal();
     //       }

     //       return result;

     //   }

        public static  T QueryFirst<T>(SqlResult SqlResult, Type type)
        {
            T result = default(T);
            try
            {

                result = UnitOfWork.uow.GetDbconnection().QueryFirst<T>( SqlResult.Sql, SqlResult.NamedBindings, UnitOfWork.uow.transaction);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                UnitOfWork.uow.returnConnectionToPoolInternal();
            }

            return result;

        }


        public static dynamic QueryFirst(SqlResult SqlResult, Type type)
        {
            dynamic result;
            try
            {

                result = UnitOfWork.uow.GetDbconnection().QueryFirst(SqlResult.Sql, SqlResult.NamedBindings, UnitOfWork.uow.transaction);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                UnitOfWork.uow.returnConnectionToPoolInternal();
            }

            return result;

        }

        public static T QueryFirstOrDefault<T>(SqlResult SqlResult, Type type)
        {
            T result = default(T);
            try
            {

                result = UnitOfWork.uow.GetDbconnection().QueryFirstOrDefault<T>(SqlResult.Sql, SqlResult.NamedBindings, UnitOfWork.uow.transaction);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                UnitOfWork.uow.returnConnectionToPoolInternal();
            }

            return result;

        }

        public static dynamic QueryFirstOrDefault(SqlResult SqlResult)
        {
            dynamic result;
            try
            {

                result = UnitOfWork.uow.GetDbconnection().QueryFirstOrDefault(SqlResult.Sql, SqlResult.NamedBindings, UnitOfWork.uow.transaction);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                UnitOfWork.uow.returnConnectionToPoolInternal();
            }

            return result;

        }

        public static GridReader QueryMultiple(SqlResult SqlResult, int? commandTimeout = null, CommandType? commandType = null)
        {
            dynamic result;
            try
            {

                result = UnitOfWork.uow.GetDbconnection().QueryMultiple(SqlResult.Sql, SqlResult.NamedBindings, UnitOfWork.uow.transaction, commandTimeout, commandType);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                UnitOfWork.uow.returnConnectionToPoolInternal();
            }

            return result;
        }


        public static T QuerySingle<T>(SqlResult SqlResult, int? commandTimeout = null, CommandType? commandType = null)
        {
            T result = default(T);
            try
            {

                result = UnitOfWork.uow.GetDbconnection().QuerySingle<T>(SqlResult.Sql, SqlResult.NamedBindings, UnitOfWork.uow.transaction, commandTimeout, commandType);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                UnitOfWork.uow.returnConnectionToPoolInternal();
            }

            return result;
        }

        public static dynamic QuerySingle(SqlResult SqlResult, int? commandTimeout = null, CommandType? commandType = null)
        {
            dynamic result;
            try
            {

                result = UnitOfWork.uow.GetDbconnection().QuerySingle(SqlResult.Sql, SqlResult.NamedBindings, UnitOfWork.uow.transaction, commandTimeout, commandType);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                UnitOfWork.uow.returnConnectionToPoolInternal();
            }

            return result;
        }

        public static T QuerySingleOrDefault<T>(SqlResult SqlResult, int? commandTimeout = null, CommandType? commandType = null)
        {
            T result = default(T);
            try
            {

                result = UnitOfWork.uow.GetDbconnection().QuerySingleOrDefault<T>(SqlResult.Sql, SqlResult.NamedBindings, UnitOfWork.uow.transaction, commandTimeout, commandType);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                UnitOfWork.uow.returnConnectionToPoolInternal();
            }

            return result;
        }

        public static dynamic QuerySingleOrDefault(SqlResult SqlResult, int? commandTimeout = null, CommandType? commandType = null)
        {
            dynamic result;
            try
            {

                result = UnitOfWork.uow.GetDbconnection().QuerySingleOrDefault(SqlResult.Sql, SqlResult.NamedBindings, UnitOfWork.uow.transaction, commandTimeout, commandType);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                UnitOfWork.uow.returnConnectionToPoolInternal();
            }

            return result;
        }

    }
}
