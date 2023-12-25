using SV20T1080048.DomainModels;

namespace SV20T1080048.Web.Models
{
    public class PaginationSearchCustomer : PaginationSearchBaseResult
    {
        public IList<Customer> Data { get; set; }
    }
}
