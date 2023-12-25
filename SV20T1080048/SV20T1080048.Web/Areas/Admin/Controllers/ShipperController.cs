using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SV20T1080048.BusinessLayers;
using SV20T1080048.DomainModels;
using SV20T1080048.Web.AddCodes;
using SV20T1080048.Web.Models;

namespace SV20T1080048.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = $"{WebUserRoles.Administrator}")]
    [Area("Admin")]
    public class ShipperController : Controller
    {
        private const int PAGE_SIZE = 10;
        private const string SHIPPER_SEARCH = "Shipper_Search";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        //public IActionResult Index(int page = 1, string searchValue = "")
        //{
        //    int rowCount = 0;
        //    var data = CommonDataService.ListOfShippers(out rowCount, page, PAGE_SIZE, searchValue ?? "");
        //    var model = new PaginationSearchShipper()
        //    {
        //        Page = page,
        //        PageSize = PAGE_SIZE,
        //        SearchValue = searchValue ?? "",
        //        RowCount = rowCount,
        //        Data = data
        //    };
        //    string? errorMessage = Convert.ToString(TempData["ErrorMessage"]);
        //    ViewBag.ErrorMessage = errorMessage;
        //    return View(model);
        //}

        public IActionResult Index()
        {
            var input = ApplicationContext.GetSessionData<PaginationSearchInput>(SHIPPER_SEARCH);
            if (input == null)
            {
                input = new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = ""
                };
            }
            return View(input);
        }


        public IActionResult Search(PaginationSearchInput input)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfShippers(
                                            out rowCount,
                                            input.Page,
                                            input.PageSize,
                                            input.SearchValue ?? ""
                                            );
            var model = new PaginationSearchShipper()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue ?? "",
                RowCount = rowCount,
                Data = data
            };

            ApplicationContext.SetSessionData(SHIPPER_SEARCH, model);

            return View(model);
        }
        public IActionResult Create()
        {
            var model = new Shipper()
            {
                ShipperId = 0
            };
            ViewBag.Title = "Bổ sung người giao hàng";
            return View(model);
        }
        public IActionResult Edit(int id = 0)
        {
            var model = CommonDataService.GetShipper(id);
            if (model == null)
                return RedirectToAction("Index");
            ViewBag.Title = "Cập nhật người giao hàng";
            return View("Create", model);
        }
        public IActionResult Save(Shipper data)
        {
            ViewBag.Title = data.ShipperId == 0 ? "Bổ sung nguời giao hàng" : "Cập nhật người giao hàng";

            if(string.IsNullOrWhiteSpace(data.ShipperName))
            {
                ModelState.AddModelError(nameof(data.ShipperName), "Tên nhân viên giao hàng không được để trống");
            }
            if(string.IsNullOrWhiteSpace(data.Phone))
            {
                ModelState.AddModelError(nameof(data.Phone), "Số điện thoại không được để trống");
            }
            if(!ModelState.IsValid)
            {
                return View("Create", data);
            }

            if (data.ShipperId == 0)
            {
                int shipperID = CommonDataService.AddShipper(data);
                if (shipperID > 0)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.ErrorMessage = "Không bổ sung được dữ liệu";
                return View("Create", data);
            }
            else
            {
                bool success = CommonDataService.UpdateShipper(data);
                if (success)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.ErrorMessage = "Không cập nhật được dữ liệu";
                return View("Create", data);
            }
        }
        public IActionResult Delete(int id = 0)
        {
            if (Request.Method == "POST")
            {
                bool success = CommonDataService.DeleteShipper(id);
                if (!success)
                    TempData["ErrorMessage"] = "Không thể xóa loại hàng này";
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetShipper(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
    }
}
