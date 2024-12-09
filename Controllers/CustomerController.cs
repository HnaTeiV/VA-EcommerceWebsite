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
using VA_EcommerceWebsite.ViewModels;

namespace VA_EcommerceWebsite.Controllers
{
    public class CustomerController : Controller
    {
        private readonly VAEcommerceContext db;
        private readonly IMapper _mapper;
        public CustomerController(VAEcommerceContext context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }

        [HttpGet("/Customer/Register")]
        [HttpPost("/Customer/Register")]
        public IActionResult Register(RegisterVM model, IFormFile Hinh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var khachHang = _mapper.Map<KhachHang>(model);
                    khachHang.RandomKey = Util.GenerateRandomKey();
                    khachHang.MatKhau = model.MatKhau.ToMd5Hash(khachHang.MatKhau);
                    khachHang.HieuLuc = true;
                    khachHang.VaiTro = 0;
                    if (Hinh != null)
                    {
                        khachHang.Hinh = Util.UploadImage(Hinh, "KhachHang");
                    }
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

        public IActionResult Login(string? returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost("/Customer/Login")]
        public async Task<IActionResult> Login(LoginVM model, string? returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var khachHang = db.KhachHangs.SingleOrDefault(kh => kh.MaKh == model.MaKh);
                if (khachHang == null)
                {
                    ModelState.AddModelError("Lỗi", "Tài khoản không tồn tại");
                }
                else
                {
                    if (!khachHang.HieuLuc)
                    {
                        ModelState.AddModelError("Lỗi", "Tài khoản của bạn hiện đang bị khóa vui lòng liên hệ bộ phận hỗ trợ");
                    }
                    else
                    {
                        var hashPassword = model.MatKhau.ToMd5Hash(khachHang.RandomKey);
                        if (khachHang.MatKhau != hashPassword)
                        {
                            ModelState.AddModelError("Lỗi", "Sai thông tin đăng nhập");
                        }
                        else
                        {
                            var claims = new List<Claim>{
                                new Claim(ClaimTypes.Email,khachHang.Email),
                                new Claim(ClaimTypes.Name,khachHang.HoTen),
                                new Claim(ClaimTypes.Role,"Customer"),
                                new Claim("Mã khách hàng",khachHang.MaKh)
                            };
                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                            await HttpContext.SignInAsync(claimsPrincipal);
                            if (Url.IsLocalUrl(returnUrl))
                            {
                                return Redirect(returnUrl);
                            }
                            else
                            {
                                return Redirect("/");
                            }
                        }
                    }
                }
            }
            return View();
        }


        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Logout(){
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login","Customer");
        }
    }
}

