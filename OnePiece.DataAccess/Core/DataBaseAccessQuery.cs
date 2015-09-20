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
    class DataBaseAccessQuery
    {
        public static SqlConnection CreateConnection()
        {
            SqlConnection connection = new SqlConnection(ConfigurationInfo.ConnectionStringQuery);
            if (connection.State != ConnectionState.Open)
                connection.Open();

            return connection;
        }

        /// <summary>
        /// 是否存在数据
        /// eg：SELECT COUNT(1) FROM T_Customer
        /// </summary>
        /// <param name="sql">T-SQL语句</param>
        /// <param name="param">配置参数和参数数据类型</param>
        public static bool ExecuteScalar(string sql, DynamicParameters param)
        {
            int result = ExecuteScalarForInt(sql, param);

            return result > 0 ? true : false;
        }

        /// <summary>
        /// 返回一行一列
        /// eg：SELECT COUNT(1) FROM T_Customer
        /// </summary>
        /// <param name="sql">T-SQL语句</param>
        /// <param name="param">配置参数和参数数据类型</param>
        public static int ExecuteScalarForInt(string sql, DynamicParameters param)
        {
            using (IDbConnection connection = CreateConnection())
            {
                int result = connection.ExecuteScalar<int>(sql, param);

                return result;
            }

        }


    }

}
