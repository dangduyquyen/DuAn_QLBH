using SV20T1080048.DataLayers.SQLServer;
using SV20T1080048.DomainModels;

namespace SV20T1080048.Web.Models
{
    public class PaginationSearchSupplier : PaginationSearchBaseResult
    {
        public IList<Supplier> Data { get; set; }
    }
}
