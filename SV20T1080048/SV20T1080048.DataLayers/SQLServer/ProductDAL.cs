using Azure;
using Dapper;
using SV20T1080048.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV20T1080048.DataLayers.SQLServer
{
    public class ProductDAL : _BaseDAL, IProductDAL
    {
        public ProductDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Product data)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"if exists(select * from Products where ProductName = @ProductName)
                                select -1
                            else
                                begin
                                    insert into Products(ProductName, ProductDescription, SupplierID, CategoryID, Unit, Price, Photo, IsSelling)
                                    values(@ProductName,@ProductDescription,@SupplierID,@CategoryID,@Unit,@Price,@Photo,@IsSelling);
                                    select @@identity;
                                end";
                var parameters = new
                {
                    productName = data.ProductName ?? "",
                    productDescription = data.ProductDescription ?? "",
                    supplierID = data.SupplierID,
                    categoryID = data.CategoryID,
                    unit = data.Unit ?? "",
                    price = data.Price,
                    photo = data.Photo ?? "",
                    isSelling = data.IsSelling
                };
                id = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return id; 
        }

        public long AddAttribute(ProductAttribute data)
        {
            throw new NotImplementedException();
        }

        public long AddPhoto(ProductPhoto data)
        {
            throw new NotImplementedException();
        }

        public int Count(string searchValue = "", int categoryID = 0, int supplierID = 0, decimal minPrice = 0, decimal maxPrice = 0)
        {
            int count = 0;
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @"select count(*) from Products 
                            where (@searchValue = N'') or (ProductName like @searchValue)";
                var parameters = new { searchValue = searchValue };
                count = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return count;
        }

        public bool Delete(int productID)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"
            DELETE FROM OrderDetails WHERE ProductID = @productId;
            DELETE FROM ProductPhotos WHERE ProductID = @productId;
            DELETE FROM ProductAttributes WHERE ProductID = @productId;
            DELETE FROM Products WHERE ProductID = @productId;
        ";
                var parameters = new { productId = productID };
                result = connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public bool DeleteAttribute(long attributeID)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"
                    DELETE FROM ProductAttributes WHERE AttributeID = attributeID";
                var parameters = new { attributeID = attributeID };
                result = connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public bool DeletePhoto(long photoID)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"
                    DELETE FROM Product WHERE PhotoID = photoID";
                var parameters = new { photoID = photoID };
                result = connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public Product? Get(int productID)
        {
            Product? data = null;
            using (var connection = OpenConnection())
            {
                var sql = "select * from Products where ProductId = @productId";
                var parameters = new { productId = productID };
                data = connection.QueryFirstOrDefault<Product>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return data;
        }

        public ProductAttribute? GetAttribute(long attributeID)
        {
            ProductAttribute? data = null;
            using (var connection = OpenConnection())
            {
                var sql = "select * from ProductAttributes where AttributeID = @attributeID";
                var parameters = new { attributeID = attributeID };
                data = connection.QueryFirstOrDefault<ProductAttribute>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return data;
        }

        public ProductPhoto? GetPhoto(long photoID)
        {
            ProductPhoto? data = null;
            using (var connection = OpenConnection())
            {
                var sql = "select * from ProductPhotos where PhotoID = @photoID";
                var parameters = new { photoID = photoID };
                data = connection.QueryFirstOrDefault<ProductPhoto>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return data;
        }

        public bool InUsed(int productID)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"if exists(select * from ProductAttributes where ProductId = @productId)
                                select 1
                            else 
                                select 0";
                var parameters = new { productId = productID };
                result = connection.ExecuteScalar<bool>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return result;
        }

        public IList<Product> List(int page = 1, int pageSize = 0, string searchValue = "", int categoryID = 0, int supplierID = 0, decimal minPrice = 0, decimal maxPrice = 0)
        {
            List<Product> data;
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @"with cte as
                            (
	                            select	*, row_number() over (order by ProductName) as RowNumber
	                            from	Products 
	                            where	(@searchValue = N'') or (ProductName like @searchValue)
                            )
                            select * from cte
                            where  (@pageSize = 0) 
	                            or (RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize)
                            order by RowNumber";
                var parameters = new
                {
                    page = page,
                    pageSize = pageSize,
                    searchValue = searchValue,
                    categoryID = categoryID,
                    supplierID = supplierID,
                };
                data = (connection.Query<Product>(sql: sql, param: parameters, commandType: CommandType.Text)).ToList();
                connection.Close();
            }
            if (data == null)
                data = new List<Product>();
            return data;
        }

        public IList<ProductAttribute> ListAttributes(int productID)
        {
            List<ProductAttribute> data;
            using (var connection = OpenConnection())
            {
                var sql = @"SELECT * FROM ProductAttributes WHERE ProductID = @ProductID ORDER BY DisplayOrder ASC";
                var parameters = new
                {
                    productID = productID,   
                };
                data = (connection.Query<ProductAttribute>(sql: sql, param: parameters, commandType: CommandType.Text)).ToList();
                connection.Close();
            }
            if (data == null)
                data = new List<ProductAttribute>();
            return data;
        }

        public IList<ProductPhoto> ListPhotos(int productID)
        {
            List<ProductPhoto> data;
            using (var connection = OpenConnection())
            {
                var sql = @"SELECT * FROM ProductPhotos WHERE ProductID = @ProductID ORDER BY DisplayOrder ASC";
                var parameters = new
                {
                    productID = productID,
                };
                data = (connection.Query<ProductPhoto>(sql: sql, param: parameters, commandType: CommandType.Text)).ToList();
                connection.Close();
            }
            if (data == null)
                data = new List<ProductPhoto>();
            return data;
        }

        public bool Update(Product data)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAttribute(ProductAttribute data)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePhoto(ProductPhoto data)
        {
            throw new NotImplementedException();
        }
    }
}
