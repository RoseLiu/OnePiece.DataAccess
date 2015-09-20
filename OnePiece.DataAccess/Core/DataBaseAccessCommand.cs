using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.DataAccess.Core
{
    using System.Data;
    using System.Data.SqlClient;
    using Dapper;


    /// <summary>
    /// 刘裕惠 
    /// .NET开发工程师
    /// </summary>
    public partial class DataBaseAccessCommand
    {
        private static SqlConnection CreateConnection()
        {
            SqlConnection connection = new SqlConnection(ConfigurationInfo.ConnectionStringCommand);
            if (connection.State != ConnectionState.Open)
                connection.Open();

            return connection;
        }


        /// <summary>
        /// 执行（增、删、改）操作方法
        /// </summary>
        /// <param name="sql">T-SQL语句</param>
        /// <param name="param">配置参数和参数数据类型</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteCommand(string sql, DynamicParameters param)
        {
            int result = 0;

            IDbConnection connection = null;
            try
            {
                connection = CreateConnection();

                result = connection.Execute(sql, param);
            }
            catch (Exception)
            {
                result = -1;

                //记录日志
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }



            return result;
        }


        /// <summary>
        /// 执行（增、删、改）操作方法
        /// 附带事务
        /// </summary>
        /// <param name="sql">T-SQL语句</param>
        /// <param name="param">配置参数和参数数据类型</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteCommand_Trans(string sql, DynamicParameters param)
        {
            int result = 0;

            IDbConnection connection = null;
            IDbTransaction trans = null;
            try
            {
                connection = CreateConnection();
                trans = connection.BeginTransaction();

                result = connection.Execute(sql, param, trans);
                trans.Commit();
            }
            catch (Exception)
            {
                result = -1;
                trans.Rollback();

                //记录日志
            }
            finally
            {
                if (trans != null) { trans.Dispose(); }
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            return result;
        }


        /// <summary>
        /// 执行（增、删、改）操作方法
        /// 附带自定义事务锁定行为
        /// </summary>
        /// <param name="sql">T-SQL语句</param>
        /// <param name="param">配置参数和参数数据类型</param>
        /// <param name="transLevel"></param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteCommand_Trans(string sql, DynamicParameters param, IsolationLevel transLevel)
        {
            int result = 0;

            IDbConnection connection = null;
            IDbTransaction trans = null;
            try
            {
                connection = CreateConnection();
                trans = connection.BeginTransaction(transLevel);

                result = connection.Execute(sql, param, trans);
                trans.Commit();
            }
            catch (Exception)
            {
                result = -1;
                trans.Rollback();

                //记录日志
            }
            finally
            {
                if (trans != null) { trans.Dispose(); }
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            return result;
        }

    }


    /// <summary>
    /// 存储过程
    /// </summary>
    public partial class DataBaseAccessCommand
    {

        /// <summary>
        /// 执行（增、删、改）操作方法
        /// </summary>
        /// <param name="sql">存储过程名称</param>
        /// <param name="param">配置参数和参数数据类型</param>
        public static int ExecuteCommandByStoredProcedure(string sql, DynamicParameters param)
        {
            int result = 0;

            IDbConnection connection = null;
            try
            {
                connection = CreateConnection();

                result = connection.Execute(sql, param, null, null, CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                result = -1;

                //记录日志
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            return result;
        }


        /// <summary>
        /// 执行（增、删、改）操作方法
        /// 附带事务
        /// </summary>
        /// <param name="sql">存储过程名称</param>
        /// <param name="param">配置参数和参数数据类型</param>
        public static int ExecuteCommandByStoredProcedure_Trans(string sql, DynamicParameters param)
        {
            int result = 0;

            IDbConnection connection = null;
            IDbTransaction trans = null;
            try
            {
                connection = CreateConnection();
                trans = connection.BeginTransaction();

                result = connection.Execute(sql, param, trans, null, CommandType.StoredProcedure);
                trans.Commit();
            }
            catch (Exception)
            {
                result = -1;
                trans.Rollback();

                //记录日志
            }
            finally
            {
                if (trans != null) { trans.Dispose(); }
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }



            return result;
        }


        /// <summary>
        /// 执行（增、删、改）操作方法
        /// 附带自定义事务锁定行为
        /// </summary>
        /// <param name="sql">存储过程名称</param>
        /// <param name="param">配置参数和参数数据类型</param>
        public static int ExecuteCommandByStoredProcedure_Trans(string sql, DynamicParameters param, IsolationLevel transLevel)
        {
            int result = 0;

            IDbConnection connection = null;
            IDbTransaction trans = null;
            try
            {
                connection = CreateConnection();
                trans = connection.BeginTransaction(transLevel);

                result = connection.Execute(sql, param, trans, null, CommandType.StoredProcedure);
                trans.Commit();
            }
            catch (Exception)
            {
                result = -1;
                trans.Rollback();

                //记录日志
            }
            finally
            {
                if (trans != null) { trans.Dispose(); }
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            return result;
        }


    }


    /// <summary>
    /// 批量操作
    /// </summary>
    public partial class DataBaseAccessCommand
    {
        /// <summary>
        /// 批量命令
        /// 附带事务
        /// </summary>
        /// <param name="sqlList">T-SQL 集合</param>
        /// <param name="paramList">参数集合</param>
        public static int ExecuteCommandBatch_Trans(List<string> sqlList, List<DynamicParameters> paramList)
        {
            int result = 0;

            IDbConnection connection = null;
            IDbTransaction trans = null;

            try
            {
                connection = CreateConnection();
                trans = connection.BeginTransaction();

                for (int i = 0, j = 0; i < sqlList.Count && j < paramList.Count; i++, j++)
                {
                    result += connection.Execute(sqlList[i], paramList[j], trans, null, CommandType.Text);
                }

                trans.Commit();
            }
            catch (Exception)
            {
                result = -1;
                trans.Rollback();

                //记录日志
            }
            finally
            {
                if (trans != null) { trans.Dispose(); }
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }



            return result;
        }


        /// <summary>
        /// 批量命令
        /// 附带自定义事务锁定行为
        /// </summary>
        /// <param name="sqlList">T-SQL 集合</param>
        /// <param name="paramList">参数集合</param>
        public static int ExecuteCommandBatch_Trans(List<string> sqlList, List<DynamicParameters> paramList, IsolationLevel transLevel)
        {
            int result = 0;

            IDbConnection connection = null;
            IDbTransaction trans = null;

            try
            {
                connection = CreateConnection();
                trans = connection.BeginTransaction(transLevel);

                for (int i = 0, j = 0; i < sqlList.Count && j < paramList.Count; i++, j++)
                {
                    result += connection.Execute(sqlList[i], paramList[j], trans, null, CommandType.Text);
                }

                trans.Commit();
            }
            catch (Exception)
            {
                result = -1;
                trans.Rollback();

                //记录日志
            }
            finally
            {
                if (trans != null) { trans.Dispose(); }
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }



            return result;
        }



        /// <summary>
        /// 批量命令
        /// 附带事务
        /// </summary>
        /// <param name="sqlList">存储过程名称 集合</param>
        /// <param name="paramList">参数集合</param>
        public static int ExecuteCommandByStoredProcedureBatch_Trans(List<string> sqlList, List<DynamicParameters> paramList)
        {
            int result = 0;

            IDbConnection connection = null;
            IDbTransaction trans = null;

            try
            {
                connection = CreateConnection();
                trans = connection.BeginTransaction();

                for (int i = 0, j = 0; i < sqlList.Count && j < paramList.Count; i++, j++)
                {
                    result += connection.Execute(sqlList[i], paramList[j], trans, null, CommandType.StoredProcedure);
                }

                trans.Commit();
            }
            catch (Exception)
            {
                result = -1;
                trans.Rollback();

                //记录日志
            }
            finally
            {
                if (trans != null) { trans.Dispose(); }
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            return result;
        }


    }

}
