using System.Net;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using VA_EcommerceWebsite.Data;
using VA_EcommerceWebsite.Interface;
using VA_EcommerceWebsite.Mappers;
using VA_EcommerceWebsite.ViewModels;

namespace VA_EcommerceWebsite.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly VAEcommerceContext db;
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _repo;
        public EmployeeController(VAEcommerceContext context, IMapper mapper, IEmployeeRepository repo)
        {
            db = context;
            _mapper = mapper;
            _repo = repo;
        }
        [AllowAnonymous]
        [HttpGet("/Employee/Login")]
        public IActionResult EmployeeLogin(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost("/Employee/Login")]
        public async Task<IActionResult> EmployeeeLoginAsync(LoginVM model, string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            if (!ModelState.IsValid)
            {
                return View("EmployeeLogin", model);
            }
            var employee = db.NhanViens.FirstOrDefault(p => model.TaiKhoan == p.Email);
            if (employee == null)
            {
                ModelState.AddModelError("Lỗi", "Sai thông tin đăng nhập hoặc tài khoản bị khóa.");
                return View(model);
            }
            if (model.MatKhau != employee.MatKhau)
            {
                ModelState.AddModelError("Lỗi", "Sai thông tin đăng nhập hoặc tài khoản bị khóa.");
                return View(model);
            }
            var claims = new List<Claim>{
                new Claim(ClaimTypes.Name,employee.HoTen),
                new Claim(ClaimTypes.Email,employee.Email),
                new Claim(ClaimTypes.MobilePhone,employee.Sdt),
                new Claim(ClaimTypes.Gender,employee.GioiTinh==false?"Nam":"Nữ"),
                new Claim(ClaimTypes.Role,employee.ChucVu)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync("EmployeeAuth", claimsPrincipal);

            if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
            {
                return Redirect(ReturnUrl);
            }

            return RedirectToAction("DashBoard");
        }
        [Authorize(AuthenticationSchemes = "EmployeeAuth", Roles = "ADMIN,MANAGER")]
        public IActionResult DashBoard()
        {
            return View();
        }
        [Authorize(AuthenticationSchemes = "EmployeeAuth", Roles = "ADMIN,MANAGER")]
        public IActionResult Products()
        {
            var prod = db.HangHoas.Select(p => p.ToProductManageDto()).ToList();

            return View(prod);
        }
        [Authorize(AuthenticationSchemes = "EmployeeAuth", Roles = "ADMIN,MANAGER")]
        [HttpPost("/Employee/AddProduct")]
        public async Task<IActionResult> AddProduct(ProductsManageVM model)
        {
            Console.WriteLine("Received Data: " + JsonConvert.SerializeObject(model));
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid model data." });
            }

            else
            {
                var product = db.HangHoas.FirstOrDefault(p => model.TenHh == p.TenHh);
                if (product != null)
                {
                    return Json(new { success = false, message = "Product was exist in database" });
                }
                else
                {
                    string result = await _repo.AddProduct(model); // Await the async method

                    if (result == null)
                    {
                        return Json(new { success = false, message = "Failed to add product." });
                    }

                    return Json(new { success = true, message = result });
                }

            }
        }
        [Authorize(AuthenticationSchemes = "EmployeeAuth", Roles = "ADMIN,MANAGER")]
        [HttpPost("/Employee/UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(ProductsManageVM model)
        {
            // Log the received model to ensure data is passed correctly
            Console.WriteLine("Received Data: " + JsonConvert.SerializeObject(model));
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid model data received." });
            }

            if (model.MaHh == 0)
            {
                return Json(new { success = false, message = "Invalid Product ID." });
            }
            string result = await _repo.UpdateProduct(model);
            if (result == null)
            {
                return Json(new { success = false, message = result });
            }
            return Json(new { success = true, message = result });

        }

        [Authorize(AuthenticationSchemes = "EmployeeAuth", Roles = "ADMIN,MANAGER")]
        [HttpDelete("/Employee/DeleteProduct/{id}")]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            if (id != 0)
            {
                var prod = db.HangHoas.FirstOrDefault(p => p.MaHh == id);
                if (prod != null)
                {
                    db.HangHoas.Remove(prod);
                    db.SaveChanges();
                    return Json(new { success = true, message = $"This product has been removed" });

                }
                else
                {
                    return Json(new { success = false, message = $"Can't find this prodcut" });
                }
            }
            else
            {
                return Json(new { success = false, message = "Error: product don't exist" });
            }
        }
        [Authorize(AuthenticationSchemes = "EmployeeAuth")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("EmployeeAuth");
            return RedirectToAction("EmployeeLogin", "Employee");
        }
        [Authorize(AuthenticationSchemes = "EmployeeAuth", Roles = "ADMIN,MANAGER")]
        [HttpGet("/Employee/SearchProducts")]
        public IActionResult SearchProducts(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return Json(new { success = false, message = "Keyword cannot be empty." });
            }

            keyword = keyword.ToLower().Trim(); // Normalize input

            var products = db.HangHoas
                             .Where(p => p.TenHh.ToLower().Contains(keyword) || p.TenAlias.ToLower().Contains(keyword))
                             .Select(p => new ProductsManageVM
                             {
                                 MaHh = p.MaHh,
                                 TenHh = p.TenHh,
                                 MaLoai = p.MaLoai,
                                 MoTaDonVi = p.MoTaDonVi,
                                 DonGia = (double)p.DonGia,
                                 NgaySX = p.NgaySx,
                                 GiamGia = p.GiamGia,
                                 SoLanXem = p.SoLanXem,
                                 MaNCC = p.MaNcc,
                                 TenAlias = p.TenAlias,
                                 Hinh = p.Hinh,
                                 MoTa = p.MoTa
                             })
                             .ToList();

            if (!products.Any())
            {
                return Json(new { success = false, message = "No products found." });
            }

            return Json(new { success = true, data = products });
        }

        [Authorize(AuthenticationSchemes = "EmployeeAuth", Roles = "ADMIN")]
        public IActionResult Employees()
        {
            var Employees = db.NhanViens.Select(p => p.ToEmployeeDto()).ToList();
            return View(Employees);
        }
        [Authorize(AuthenticationSchemes = "EmployeeAuth", Roles = "ADMIN,MANAGER")]
        [HttpPost("/Employee/UpdateEmployee")]
        public IActionResult UpdateEmployee(EmployeeVM model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage)
                                  .ToList();

                return Json(new { success = false, message = "Invalid model data received.", errors });
            }
            var employee = db.NhanViens.FirstOrDefault(p => p.MaNv == model.MaNv);
            if (employee == null)
            {
                return Json(new { success = false, message = $"Employee not found" });

            }
            if (db.Entry(employee).State == EntityState.Detached)
            {
                db.Attach(employee);
            }

            // Update necessary fields
            employee.HoTen = model.HoTen;
            employee.Email = model.Email;
            employee.GioiTinh = model.GioiTinh;
            employee.MaPb = model.MaPb;
            employee.NgayVaoLam = DateTime.Now;
            employee.ChucVu = model.ChucVu;
            employee.MatKhau = model.MatKhau;
            employee.Luong = model.Luong;
            employee.Sdt = model.Sdt;
            employee.IsActive = model.IsActive;

            try
            {
                int rowsAffected = db.SaveChanges();

                if (rowsAffected == 0)
                {
                    return Json(new { success = false, message = $"No changes were made." });

                }
                return Json(new { success = true, message = $"Product updated successfully!" });

            }
            catch (DbUpdateException ex)
            {
                return Json(new { success = false, message = $"Error: {ex.InnerException?.Message ?? ex.Message}" });
            }
        }
        [Authorize(AuthenticationSchemes="EmployeeAuth",Roles ="ADMIN,MANAGER")]
        [HttpDelete("/Employee/DeleteEmployee/{id}")]
        public IActionResult DeleteEmployee([FromRoute]int id){
            if(id==0){
                return Json(new{success=false,message=$"Can't receive data from API"});;
            }
            var emp=db.NhanViens.FirstOrDefault(p=>p.MaNv==id);
            if(emp==null){
                return Json(new{success=false,message=$"Can't find employee in database"});        
            }
            db.NhanViens.Remove(emp);
            db.SaveChanges();
            return Json(new{success=true,message=$"Delete employee success"});
        }
        [AllowAnonymous]
        [HttpGet("/Employee/AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}