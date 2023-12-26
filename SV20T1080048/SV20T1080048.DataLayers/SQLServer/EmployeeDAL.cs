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
    public class EmployeeDAL : _BaseDAL, ICommonDAL<Employee>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public EmployeeDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Employee data)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"if exists(select * from Employees where Email = @Email)
                                select -1
                            else
                                begin
                                    insert into Employees (FullName, BirthDate,Address,Phone,Email,Password,Photo, isWorking)
                                    values(@Fullname,@BirthDate,@Address,@Phone,@Email,@Password,@Photo, @isWorking);
                                    select @@identity;
                                end";
                var parameters = new
                {
                    fullName = data.FullName ?? "",
                    birthDate = data.BirthDate,
                    address = data.Address ?? "",
                    phone = data.Phone ?? "",
                    email = data.Email ?? "",
                    password = data.Password ?? "",
                    photo = data.Photo ?? "",
                    isWorking = data.IsWorking
                };
                id = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }

                return id;
        }

        public int Count(string searchValue = "")
        {
            int count = 0;
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @"select count(*) from Employees 
                            where (@searchValue = N'') or (FullName like @searchValue)";
                var parameters = new { searchValue = searchValue };
                count = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return count;
        }

        public bool Delete(int id)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = "delete from Employees where EmployeeId = @employeeId and not exists(select * from Orders where EmployeeId = @employeeId)";
                var parameters = new { employeeId = id };
                result = connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public Employee? Get(int id)
        {
            Employee? data = null;
            using (var connection = OpenConnection())
            {
                var sql = "select * from Employees where EmployeeId = @employeeId";
                var parameters = new { employeeId = id };
                data = connection.QueryFirstOrDefault<Employee>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return data;
        }

        public bool InUsed(int id)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"if exists(select * from Orders where EmployeeId = @employeeId)
                                select 1
                            else 
                                select 0";
                var parameters = new { employeeId = id };
                result = connection.ExecuteScalar<bool>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return result;
        }

        public IList<Employee> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            List<Employee> data;
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @"with cte as
                            (
	                            select	*, row_number() over (order by FullName) as RowNumber
	                            from	Employees 
	                            where	(@searchValue = N'') or (FullName like @searchValue)
                            )
                            select * from cte
                            where  (@pageSize = 0) 
	                            or (RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize)
                            order by RowNumber";
                var parameters = new
                {
                    page = page,
                    pageSize = pageSize,
                    searchValue = searchValue
                };
                data = (connection.Query<Employee>(sql: sql, param: parameters, commandType: CommandType.Text)).ToList();
                connection.Close();
            }
            if (data == null)
                data = new List<Employee>();
            return data;
        }

        public bool Update(Employee data)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"if not exists(select * from Employees where EmployeeID <> @employeeID and Email = @email)
                        begin
                            update Employees 
                            set FullName = @fullName,
                                BirthDate = @birthDate,
                                Address = @address,
                                Phone = @phone,
                                Email = @email,
                                Photo = @photo,
                                IsWorking = @isWorking
                            where EmployeeID = @employeeID
                        end";

                var parameters = new
                {
                    employeeID = data.EmployeeID,
                    fullName = data.FullName ?? "",
                    birthDate = data.BirthDate,
                    address = data.Address ?? "",
                    phone = data.Phone ?? "",
                    email = data.Email ?? "",
                    photo = data.Photo ?? "",
                    isWorking = data.IsWorking
                };

                // Thực hiện truy vấn và cập nhật kết quả
                result = connection.Execute(sql, parameters) > 0;
            }

            return result;
        }


    }
}
