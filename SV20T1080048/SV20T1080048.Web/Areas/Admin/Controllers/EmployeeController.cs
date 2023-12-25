using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SV20T1080048.BusinessLayers;
using SV20T1080048.DomainModels;
using SV20T1080048.Web.AddCodes;
using SV20T1080048.Web.Models;
using System.Drawing.Printing;
using System.Reflection;

namespace SV20T1080048.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = $"{WebUserRoles.Administrator}")]
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private const int PAGE_SIZE = 9;
        private const string EMPLOYEE_SEARCH = "Employee_Search";
        //public IActionResult Index(int page = 1, string searchValue = "")
        //{
        //    int rowCount = 0;
        //    var data = CommonDataService.ListOfEmployees(out rowCount, page, PAGE_SIZE, searchValue ?? "");
        //    var model = new PaginationSearchEmployee()
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
            var input = ApplicationContext.GetSessionData<PaginationSearchInput>(EMPLOYEE_SEARCH);
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
            var data = CommonDataService.ListOfEmployees(
                                            out rowCount,
                                            input.Page,
                                            input.PageSize,
                                            input.SearchValue ?? ""
                                            );
            var model = new PaginationSearchEmployee()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue ?? "",
                RowCount = rowCount,
                Data = data
            };

            ApplicationContext.SetSessionData(EMPLOYEE_SEARCH, model);

            return View(model);
        }

        public IActionResult Create()
        {
            var model = new Employee()
            {
                EmployeeID = 0
            };
            ViewBag.Title = "Bổ sung nhân viên";
            return View(model);

    }
        public IActionResult Edit(int id = 0)
        {
            Employee model = new Employee()
            {
                EmployeeID = 123,
                FullName = "Trần Nguyên Phong",
                Address = "77 Nguyễn Huệ",
                BirthDate = new DateTime(1976, 12, 20),
                Email = "tnphong@gmail.com",
                Phone = "0935254782",
                IsWorking = true,
                Photo = "photo.png"
            };

            ViewBag.Title = "Cập nhật nhân viên";
            return View("Create", model);
        }
        public IActionResult Save(Employee model, string birthday, IFormFile? uploadPhoto)
        {



            //Xử lý ngày sinh
            DateTime? dBirthDate = Converter.StringToDateTime(birthday);
            if (dBirthDate == null)
                ModelState.AddModelError(nameof(model.BirthDate), "Ngày sinh không hợp lệ");
            else
                model.BirthDate = dBirthDate.Value;

            //Xử lý với ảnh
            //Upload ảnh lên (nếu có), sau khi upload xong thì mới lấy tên file ảnh vừa upload
            //để gán cho trường Photo của Employee
            if (uploadPhoto != null)
            {
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(ApplicationContext.HostEnviroment.WebRootPath, @"images\employees", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    uploadPhoto.CopyTo(stream);
                }
                model.Photo = fileName;
            }

            //Kiểm tra đầu vào của model

            if (!ModelState.IsValid)
                return Content("Có lỗi xảy ra");

            //Lưu dữ liệu (lưu model vào database)
            if (model.EmployeeID == 0)
            {
                CommonDataService.AddEmployee(model);
            }
            else
            {
                CommonDataService.UpdateEmployee(model);
            }
            return RedirectToAction("Index");
            

        }
        public IActionResult Delete(int id = 0)
        {
            if (Request.Method == "POST")
            {
                bool success = CommonDataService.DeleteEmployee(id);
                if (!success)
                    TempData["ErrorMessage"] = "Không thể xóa khách hàng này";
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetEmployee(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        public IActionResult ChangePassword(int id = 0)
        {
            
            return View();
        }
    }
}
