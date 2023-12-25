using SV20T1080048.BusinessLayers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SV20T1080048.Web
{
    public class SelectListHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Province()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "",
                Text = " -- Chọn tỉnh thành --"
            });
            foreach (var item in CommonDataService.ListOfProvinces())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.ProvinceName,
                    Text = item.ProvinceName,
                });
            }
            return list;
        }

        public static List<SelectListItem> categories()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "--Loại hàng--"
            });
            foreach (var item in CommonDataService.ListOfCategories())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CategoryID.ToString(),
                    Text = item.CategoryName
                });
            }
            return list;
        }

        /// <summary>
        /// Danh sách nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> suppliers()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "--Nhà cung cấp--"
            });
            foreach (var item in CommonDataService.ListOfSuppliers())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.SupplierID.ToString(),
                    Text = item.SupplierName
                });
            }
            return list;
        }
    }
}
