using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SV20T1080048.DomainModels;

namespace SV20T1080048.DataLayers.SQLServer
{
    public class ProvinceDAL : _BaseDAL, ICommonDAL<Province>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public ProvinceDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Province data)
        {
            throw new NotImplementedException();
        }

        public int Count(string searchValue = "")
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Province? Get(int id)
        {
            throw new NotImplementedException();
        }

        public bool InUsed(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public IList<Province> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            List<Province> data;
            using (var connection = OpenConnection())
            {
                var sql = "select * from Provinces";
                data = connection.Query<Province>(sql: sql).ToList();
            }
            if (data == null) data = new List<Province>();
            return data;
        }

        public bool Update(Province data)
        {
            throw new NotImplementedException();
        }
    }
}
