using SV20T1080048.DomainModels;

namespace SV20T1080048.Web.Models
{
    public class PaginationSearchCategory : PaginationSearchBaseResult
    {
        public IList<Category> Data { get; set; }
    }
}
