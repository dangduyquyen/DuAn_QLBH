using SV20T1080048.DataLayers;
using SV20T1080048.DataLayers.SQLServer;
using SV20T1080048.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV20T1080048.BusinessLayers
{
    public static class UserAccountSevice
    {
        private static readonly IUserAccountDAL employeeUserAccountDB;
        //private static readonly IUserAccountDAL customerUserAccountDB;

        static UserAccountSevice()
        {
            string connectionString = "server=DESKTOP-HVNL2AM\\SQLEXPRESS;user id=sa;password=123123;database=LiteCommerceDB;TrustServerCertificate=true";
            employeeUserAccountDB = new DataLayers.SQLServer.EmployeeUserAccountDAL(connectionString);
            //customerUserAccountDB = new DataLayers.SQLServer.CustomerUserAccountDAL(connectionString);

        }

        public static UserAccount Authorize(string userName, string password, TypeOfAccount typeOfAccount)
        {
            switch (typeOfAccount)
            {
                case TypeOfAccount.Employee:
                    return employeeUserAccountDB.Authorize(userName, password);
                //case TypeOfAccount.Customer:
                //    return customerUserAccountDB.Authorize(userName, password);
                default:
                    return null;
            }

        }

        /// <summary>
        /// Các loại account
        /// </summary>
        /// 
        public enum TypeOfAccount
        {
            Employee,
            Customer,
            Shipper
        }
    }
}
