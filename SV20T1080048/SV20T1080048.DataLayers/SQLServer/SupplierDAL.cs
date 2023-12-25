using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SV20T1080048.DomainModels;
using Microsoft.Data.SqlClient;
using System.Data;

namespace SV20T1080048.DataLayers.SQLServer
{
    public class SupplierDAL : _BaseDAL, ICommonDAL<Supplier>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public SupplierDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Supplier data)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"if exists(select * from Suppliers where SupplierName = @SupplierName)
                                select -1
                            else
                                begin
                                    INSERT INTO Suppliers (SupplierName, ContactName, Province, Address, Phone, Email)
                                    VALUES (@SupplierName,@ContactName,@Province,@Address,@Phone,@Email);
                                    select @@identity;
                                end";
                var parameters = new
                {
                    SupplierName = data.SupplierName ?? "",
                    contactName = data.ContactName ?? "",
                    Province = data.Province ?? "",
                    Address = data.Address ?? "",
                    Phone = data.Phone ?? "",
                    Email = data.Email ?? ""

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
                var sql = @"select count(*) from Suppliers
                            where (@searchValue = N'') or (SupplierName like @searchValue)";
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
                var sql = "delete from Suppliers where SupplierId = @supplierId and not exists(select * from Products where SupplierId = @supplierId)";
                var parameters = new { supplierId = id };
                result = connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public Supplier? Get(int id)
        {
            Supplier? data = null;
            using (var connection = OpenConnection())
            {
                var sql = "select * from Suppliers where SupplierId = @supplierId";
                var parameters = new { supplierId = id };
                data = connection.QueryFirstOrDefault<Supplier>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return data;
        }

        public bool InUsed(int id)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"if exists(select * from Product where SupplierId = @supplierId)
                                select 1
                            else 
                                select 0";
                var parameters = new { supplierId = id };
                result = connection.ExecuteScalar<bool>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return result;
        }

        public IList<Supplier> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            List<Supplier> data;
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @"with cte as
                            (
	                            select	*, row_number() over (order by SupplierName) as RowNumber
	                            from	Suppliers
	                            where	(@searchValue = N'') or (SupplierName like @searchValue)
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
                data = (connection.Query<Supplier>(sql: sql, param: parameters, commandType: CommandType.Text)).ToList();
                connection.Close();
            }
            if (data == null)
                data = new List<Supplier>();
            return data;
        }

        public bool Update(Supplier data)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"if not exists(select * from Suppliers where SupplierId <> @supplierId and Email = @email)
                                begin
                                    update Suppliers
                                    set SupplierName = @supplierName,
                                        ContactName = @contactName,
                                        Province = @province,
                                        Address = @address,
                                        Phone = @phone,
                                        Email = @email
                                    where SupplierId = @supplierId
                                end";
                var parameters = new
                {
                    supplierId = data.SupplierID,
                    supplierName = data.SupplierName ?? "",
                    contactName = data.ContactName ?? "",
                    Province = data.Province ?? "",
                    Address = data.Address ?? "",
                    Phone = data.Phone ?? "",
                    Email = data.Email ?? "",
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
            }
            return result;
        }
    }
}
