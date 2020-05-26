using Dapper;
using DbPooling;
using MonoFrame.QueryGenerator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MonoFrame.DataBase
{
    public    class UnitOfWork 
    {
        [ThreadStatic]
        [NonSerialized]
        private static UnitOfWork _UnitOfWork = (UnitOfWork)null;
        [ThreadStatic]
        private static object _iconnLock = (object)null;
        [ThreadStatic]
        private static object _tranLock = (object)null;

        public  int? PoolToken;

        public int PoolTokenValid;

        public IDbConnection iDBConnection { get; private set; }

        public IDbTransaction transaction { get; private set; }

        private bool HasTransaction { get; set; }

        public bool HasExplicitTransaction { get; private set; }

        private  IsolationLevel? _isolationLevel;


        private UnitOfWork()
        {
            PoolToken = null;
        }

        public static UnitOfWork uow
        {
            get
            {
                if (UnitOfWork._UnitOfWork == null)
                    UnitOfWork._UnitOfWork = new UnitOfWork();
                return UnitOfWork._UnitOfWork;
            }
        }

        internal static object iConnLock
        {
            get
            {
                if (UnitOfWork._iconnLock == null)
                    UnitOfWork._iconnLock = new object();
                return UnitOfWork._iconnLock;
            }
        }

        internal static object iTransLock
        {
            get
            {
                if (UnitOfWork._tranLock == null)
                    UnitOfWork._tranLock = new object();
                return UnitOfWork._tranLock;
            }
        }


        internal IDbConnection GetDbconnection()
        {
            try
            {
              //  lock (UnitOfWork.iConnLock)
              //  {

                    if (UnitOfWork.uow.iDBConnection == null)
                        UnitOfWork.uow.iDBConnection = DbPool.GetConnection(out UnitOfWork.uow.PoolToken, out UnitOfWork.uow.PoolTokenValid);
                    return UnitOfWork.uow.iDBConnection;
              //  }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void OpenConnection()
        {
            try
            {
               // lock (UnitOfWork.iConnLock)
              //  {
                    if (UnitOfWork.uow.iDBConnection == null)
                        this.GetDbconnection();
                    if (UnitOfWork.uow.iDBConnection == null || UnitOfWork.uow.iDBConnection.State == ConnectionState.Open)
                        return;
                    UnitOfWork.uow.iDBConnection.Open();
              //  }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void CloseConnection()
        {
            try
            {
             //   lock (UnitOfWork.iConnLock)
               // {

                    if (UnitOfWork.uow.PoolToken != null)
                    {
                        UnitOfWork.uow.ClearIsolationLevel();
                        DbPool.FreeConnection(Convert.ToInt16(UnitOfWork.uow.PoolToken), out UnitOfWork.uow.PoolTokenValid);
                        UnitOfWork.uow.PoolToken = null;
                    } 
                    
               // }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        internal void returnConnectionToPoolInternal()
        {
          //  lock (UnitOfWork._iconnLock)
          //  {
                if(UnitOfWork.uow.HasTransaction ==false && UnitOfWork.uow.HasExplicitTransaction == false)
                {
                    DbPool.FreeConnection(Convert.ToInt16(UnitOfWork.uow.PoolToken), out UnitOfWork.uow.PoolTokenValid);
                    UnitOfWork.uow.PoolToken = null;
                }
           // }

            }




        private void CreateTransaction()
        {
            
                if (UnitOfWork.uow._isolationLevel.HasValue)
                    UnitOfWork.uow.transaction = UnitOfWork.uow.iDBConnection.BeginTransaction(_isolationLevel.GetValueOrDefault());
                else
                    UnitOfWork.uow.transaction = UnitOfWork.uow.iDBConnection.BeginTransaction();
                UnitOfWork.uow.HasTransaction = true;
            
           
        }

        internal void CreateTransactionInternal()
        {
           // lock (UnitOfWork._tranLock)
          //  {

              if(UnitOfWork.uow.HasExplicitTransaction == false && UnitOfWork.uow.HasTransaction == false)
                {
                    UnitOfWork.uow.CreateTransaction();
                }
          //  }

        }

        public void BeginTransaction()
        {
          //  lock (UnitOfWork._tranLock)
          //  {
                if (UnitOfWork.uow.transaction == null)
                {
                    this.CreateTransaction();
                    UnitOfWork.uow.HasExplicitTransaction = true;
                }
                else
                {
                    UnitOfWork.uow.HasExplicitTransaction = true;
                }

           // }
          
        }

        public void CommitTransaction()
        {

            try
            {
               // lock (UnitOfWork._tranLock)
               // {
                    if (UnitOfWork.uow.transaction != null)
                    {
                        UnitOfWork.uow.transaction.Commit();

                        UnitOfWork.uow.transaction.Dispose();
                        UnitOfWork.uow.transaction = null;
                        UnitOfWork.uow.HasExplicitTransaction = false;
                    }
                //}
            }catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                UnitOfWork.uow.CloseConnection();
            }
         

          
        }

        public void RollbackTransaction()
        {

            try
            {
              //  lock (UnitOfWork._tranLock)
               // {
                    if (UnitOfWork.uow.transaction == null) return;

                    UnitOfWork.uow.transaction.Rollback();

                    UnitOfWork.uow.transaction.Dispose();
                    UnitOfWork.uow.HasExplicitTransaction = false;
                    UnitOfWork.uow.HasTransaction = false;
              //  }

            }catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                UnitOfWork.uow.CloseConnection();
            }

        }

     
       

       
       internal void CommitInternalTransaction()
        {

          //  lock (UnitOfWork._tranLock)
           // {
                if (UnitOfWork.uow.transaction != null && UnitOfWork.uow.HasExplicitTransaction == false)
                {
                    try
                    {
                        UnitOfWork.uow.transaction.Commit();

                        UnitOfWork.uow.transaction.Dispose();
                        UnitOfWork.uow.transaction = null;
                        UnitOfWork.uow.HasTransaction = false;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        UnitOfWork.uow.CloseConnection();
                    }
                   
                }
          //  }
              
        }

        
        internal    void RollbackInternalTransaction()
        {

          //  lock (UnitOfWork._tranLock)
          //  {

                if (UnitOfWork.uow.transaction != null && UnitOfWork.uow.HasExplicitTransaction == false)
                {
                    try
                    {  
                        UnitOfWork.uow.transaction.Rollback();
                        UnitOfWork.uow.transaction.Dispose();
                        UnitOfWork.uow.transaction = null;
                        UnitOfWork.uow.HasTransaction = false;
                    }catch(Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        UnitOfWork.uow.CloseConnection();
                    }
                   
                }
           // }
             

        }

      
        public void SetIsolationLevel(IsolationLevel isolationLevel)
        {
            UnitOfWork.uow._isolationLevel = isolationLevel;
        }

        public void ClearIsolationLevel()
        {
            UnitOfWork.uow._isolationLevel = null;
        }

        public void Dispose()
        {
         //   lock (UnitOfWork._tranLock)
         //   {
                if (UnitOfWork.uow.transaction != null)
                    UnitOfWork.uow.transaction.Dispose();

                UnitOfWork.uow.transaction = null;
          //  }
          
        }


    }
}
