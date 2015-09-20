using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnePiece.DataAccess
{
    using System.Data;
    using Dapper;
    using OnePiece.Models;
    using OnePiece.DataAccess.Core;

    public class CustomerDAL
    {
        /// <summary>
        /// 添加 Customer
        /// </summary>
        public bool CustomerInsert(Customer model)
        {
            string sql = @"
                            INSERT INTO Customer
                            (
	                            Id,CustomerName,AddTime,IsDelete,CustomerLevel,
	                            PhoneNo,Token,VipPrice,Remark
                            )
                            VALUES
                            (
	                            @Id,@CustomerName,@AddTime,@IsDelete,@CustomerLevel,
	                            @PhoneNo,@Token,@VipPrice,@Remark
                            )
                            ";

            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", model.Id, DbType.Guid);
            param.Add("@CustomerName", model.CustomerName, DbType.String);
            param.Add("@AddTime", model.AddTime, DbType.DateTime);
            param.Add("@IsDelete", model.IsDelete, DbType.Boolean);
            param.Add("@CustomerLevel", model.CustomerLevel, DbType.Byte);
            param.Add("@PhoneNo", model.PhoneNo, DbType.String);
            param.Add("@Token", model.Token, DbType.String);
            param.Add("@VipPrice", model.VipPrice, DbType.Double);
            param.Add("@Remark", model.Remark, DbType.String);


            var result = DataBaseAccessCommand.ExecuteCommand(sql, param);

            return result < 0 ? false : true;
        }


        /// <summary>
        /// 修改 Customer
        /// </summary>
        public bool CustomerUpdate(Customer model)
        {
            string sql = @"
                            UPDATE Customer 
                            SET CustomerName=@CustomerName,PhoneNo=@PhoneNo,AddTime=@AddTime,Remark=@Remark
                            WHERE Id=@Id
                            ";

            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", model.Id, DbType.Guid);
            param.Add("@CustomerName", model.CustomerName, DbType.String);
            param.Add("@PhoneNo", model.PhoneNo, DbType.String);

            param.Add("@AddTime", DateTime.Now, DbType.DateTime);
            param.Add("@Remark", "测试修改", DbType.String);


            var result = DataBaseAccessCommand.ExecuteCommand(sql, param);

            return result < 0 ? false : true;
        }


        /// <summary>
        /// 删除 Customer
        /// </summary>
        public bool CustomerDelete(Guid id)
        {
            string sql = @"DELETE FROM Customer WHERE Id=@Id";
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id, DbType.Guid);

            var result = DataBaseAccessCommand.ExecuteCommand(sql, param);

            return result < 0 ? false : true;

        }


        /// <summary>
        /// 查询 Customer列表
        /// </summary>
        public IEnumerable<Customer> CustomerGetList()
        {
            string sql = @"SELECT * FROM Customer WHERE IsDelete=0";

            using (IDbConnection connection = DataBaseAccessQuery.CreateConnection())
            {
                var list = connection.Query<Customer>(sql, null);

                return list;
            }

        }


        /// <summary>
        /// 查询 Customer 单条记录
        /// </summary>
        public Customer CustomerGetModelById(Guid Id)
        {
            string sql = @"SELECT * FROM Customer WHERE IsDelete=0 AND Id=@Id";

            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", Id, DbType.Guid);

            using (IDbConnection connection = DataBaseAccessQuery.CreateConnection())
            {
                var list = connection.Query<Customer>(sql, param);

                var model = list.FirstOrDefault();

                return model;
            }
        }


    }
}
