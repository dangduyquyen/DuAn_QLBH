using SV20T1080048.DomainModels;

namespace SV20T1080048.Web.Models
{
    public class PaginationSearchShipper: PaginationSearchBaseResult
    {
        public IList<Shipper> Data { get; set; }
    }
}
