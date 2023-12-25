using SV20T1080048.DomainModels;


namespace SV20T1080048.Web.Models
{
    public class PaginationSearchEmployee: PaginationSearchBaseResult
    {
        public IList<Employee> Data { get; set; }
    }
}
