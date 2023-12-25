using SV20T1080048.DomainModels;

namespace SV20T1080048.Web.Models
{
    public class PaginationSearchProduct : PaginationSearchBaseResult
    {
        public IList<Product> Data { get; set; }
    }
}
