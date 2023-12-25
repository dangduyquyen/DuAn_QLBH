using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV20T1080048.DataLayers;
using SV20T1080048.DomainModels;


namespace SV20T1080048.BusinessLayers
{
    public static class CommonDataService
    {
        private static readonly ICommonDAL<Customer> customerDB;
        private static readonly ICommonDAL<Supplier> supplierDB;
        private static readonly ICommonDAL<Category> categoryDB;
        private static readonly ICommonDAL<Shipper> shipperDB;
        private static readonly ICommonDAL<Employee> employeeDB;
        private static readonly ICommonDAL<Province> provinceDB;
        /// <summary>
        /// 
        /// </summary>
        static CommonDataService()
        {
            string connectionString = "server=DESKTOP-HVNL2AM\\SQLEXPRESS;user id=sa;password=123123;database=LiteCommerceDB;TrustServerCertificate=true";
            customerDB = new DataLayers.SQLServer.CustomerDAL(connectionString);
            supplierDB = new DataLayers.SQLServer.SupplierDAL(connectionString);
            categoryDB = new DataLayers.SQLServer.CategoryDAL(connectionString);
            shipperDB = new DataLayers.SQLServer.ShipperDAL(connectionString);
            employeeDB = new DataLayers.SQLServer.EmployeeDAL(connectionString);
            provinceDB = new DataLayers.SQLServer.ProvinceDAL(connectionString);
        }


        public static List<Supplier> ListOfSuppliers(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "")
        {
            rowCount = supplierDB.Count(searchValue);
            return supplierDB.List(page, pageSize, searchValue).ToList();
        }

        public static List<Category> ListOfCategories(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "")
        {
            rowCount = categoryDB.Count(searchValue);
            return categoryDB.List(page, pageSize, searchValue).ToList();
        }
        public static List<Shipper> ListOfShippers(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "")
        {
            rowCount = shipperDB.Count(searchValue);
            return shipperDB.List(page, pageSize, searchValue).ToList();
        }
        public static List<Employee> ListOfEmployees(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "")
        {
            rowCount = employeeDB.Count(searchValue);
            return employeeDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Province> ListOfProvinces()
        {
            return provinceDB.List().ToList();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(out int rowCount,
                                                    int page = 1,
                                                    int pageSize = 0,
                                                    string searchValue = "")
        {
            rowCount = customerDB.Count(searchValue);
            return customerDB.List(page, pageSize, searchValue).ToList();
        }
        // => Get ID
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Customer? GetCustomer(int id)
        {
            return customerDB.Get(id);
        }
        public static Category? GetCategory(int id)
        {
            return categoryDB.Get(id);
        }
        public static Supplier? GetSupplier(int id)
        {
            return supplierDB.Get(id);
        }
        public static Shipper? GetShipper(int id)
        {
            return shipperDB.Get(id);
        }
        public static Employee? GetEmployee(int id)
        {
            return employeeDB.Get(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// 
        //=> ADD 
        public static int AddCustomer(Customer data)
        {
            return customerDB.Add(data);
        }
        public static int AddCategory(Category data)
        {
            return categoryDB.Add(data);
        }
        public static int AddSupplier(Supplier data)
        {
            return supplierDB.Add(data);
        }
        public static int AddShipper(Shipper data)
        {
            return shipperDB.Add(data);
        }
        public static int AddEmployee(Employee data)
        {
            return employeeDB.Add(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>

        // => Update
        public static bool UpdateCustomer(Customer data)
        {
            return customerDB.Update(data);
        }
        public static bool UpdateCategory(Category data)
        {
            return categoryDB.Update(data);
        }
        public static bool UpdateSupplier(Supplier data)
        {
            return supplierDB.Update(data);
        }
        public static bool UpdateShipper(Shipper data)
        {
            return shipperDB.Update(data);
        }
        public static bool UpdateEmployee(Employee data)
        {
            return employeeDB.Update(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        // => Delete
        public static bool DeleteCustomer(int id)
        {
            return customerDB.Delete(id);
        }
        public static bool DeleteCategory(int id)
        {
            return categoryDB.Delete(id);
        }
        public static bool DeleteSupplier(int id)
        {
            return supplierDB.Delete(id);
        }
        public static bool DeleteShipper(int id)
        {
            return shipperDB.Delete(id);
        }
        public static bool DeleteEmployee(int id)
        {
            return employeeDB.Delete(id);
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        // => In Used
        public static bool InUsedCustomer(int id)
        {
            return customerDB.InUsed(id);
        }
        public static bool InUsedCategory(int id)
        {
            return categoryDB.InUsed(id);
        }
        public static bool InUsedSupplier(int id)
        {
            return supplierDB.InUsed(id);
        }
        public static bool InUsedShipper(int id)
        {
            return shipperDB.InUsed(id);
        }
        public static bool InUsedEmployee(int id)
        {
            return employeeDB.InUsed(id);
        }

        public static List<Supplier> ListOfSuppliers(string searchValue = "")
        {
            return supplierDB.List(1, 0, searchValue).ToList();
        }
        public static List<Category> ListOfCategories(string searchValue = "")
        {
            return categoryDB.List(1, 0, searchValue).ToList();
        }


    }
}
