namespace SV20T1080048.Web.Models
{
    /// <summary>
    /// Thông tin đầu vào
    /// </summary>
    public class PaginationSearchInput
    {
        public int Page {  get; set; } = 1;
        public int PageSize { get; set; } = 15;
        public string SearchValue { get; set; } = "";

        /// <summary>
        /// Mã loại hàng
        /// </summary>
        public int CategoryID { get; set; }
        /// <summary>
        /// Mã nhà cung cấp
        /// </summary>
        public int SupplierID { get; set; }
    }
}
