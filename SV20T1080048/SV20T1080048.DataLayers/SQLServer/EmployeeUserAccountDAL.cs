using Dapper;
using Microsoft.Data.SqlClient;
using SV20T1080048.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV20T1080048.DataLayers.SQLServer
{
    public class EmployeeUserAccountDAL : _BaseDAL, IUserAccountDAL
    {
        public EmployeeUserAccountDAL(string connectionString) : base(connectionString)
        {
        }

        public UserAccount Authorize(string userName, string password)
        {
            UserAccount? data = null;
            using (var connection = OpenConnection())
            {
                var sql = @"Select EmployeeID as UserId, Email as UserName, FullName, Email, Photo 
                                From Employees 
                                Where Email = @userName and Password = @password";
                var parameters = new
                {
                    userName = userName, // hai tên trung nhau có thể viết thành một: userName là được
                    password = password
                };
                data = connection.QueryFirstOrDefault<UserAccount>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return data;
        }

    }
}
