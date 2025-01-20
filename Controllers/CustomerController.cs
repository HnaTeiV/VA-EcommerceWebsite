using System.Security.Claims;
using System.Security.Policy;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using VA_EcommerceWebsite.Data;
using VA_EcommerceWebsite.Helpers;
using VA_EcommerceWebsite.Interface;
using VA_EcommerceWebsite.ViewModels;

namespace VA_EcommerceWebsite.Controllers
{
    public class CustomerController : Controller
    {
        private readonly VAEcommerceContext db;
        private readonly ICustomerRepository _customerRepo;
        public CustomerController(VAEcommerceContext context, ICustomerRepository customerRepo)
        {
            db = context;
            _customerRepo = customerRepo;
        }

        [HttpGet("/Customer/Register")]
        [HttpPost("/Customer/Register")]
        [AllowAnonymous]
        public IActionResult Register(RegisterVM model, IFormFile Hinh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var khachHang = _customerRepo.Register(model, Hinh);
                    db.Add(khachHang);
                    db.SaveChanges();
                    return RedirectToAction("Index", "HangHoa");
                }
                catch (Exception ex)
                {

                }
            }
            return View();
        }
        
        [HttpGet("/Customer/Login")]
        [AllowAnonymous]
        public IActionResult Login(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost("/Customer/Login")]
        public async Task<IActionResult> Login(LoginVM model, string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var khachHang = await _customerRepo.Login(model);
            if (khachHang == null)
            {
                ModelState.AddModelError("Lỗi", "Sai thông tin đăng nhập hoặc tài khoản bị khóa.");
                return View(model);
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Email, khachHang.Email),
        new Claim(ClaimTypes.Name, khachHang.HoTen),
        new Claim("Mã khách hàng", khachHang.MaKh),
        new Claim(ClaimTypes.Role, "Customer")
    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(claimsPrincipal);

            if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
            {
                return Redirect(ReturnUrl);
            }

            return RedirectToAction("Index", "HangHoa");
        }



        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Customer");
        }
        [Authorize]
        public IActionResult GetFavourtiesPage(){
            return View();
        }
    }
}

