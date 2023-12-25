namespace SV20T1080048.Web.Models
{
    public class PaginationSearchBaseResult 
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SearchValue { get; set; } = "";
        public int RowCount { get; set; }
        public int PageCount
        {
            get
            {
                if (PageSize == 0)
                    return -1;

                int c = RowCount / PageSize;
                if (RowCount % PageSize > 0)
                    c += 1;
                return c;
            }
        }
    }
}
