using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VA_EcommerceWebsite.Data;
using VA_EcommerceWebsite.ViewModels;

namespace VA_EcommerceWebsite.ViewComponents
{
    [ViewComponent]
    public class FavourtiesViewComponent : ViewComponent
    {
        private readonly VAEcommerceContext db;
        public FavourtiesViewComponent(VAEcommerceContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var maKhachHang = HttpContext.User?.Claims.FirstOrDefault(c => c.Type == "Mã khách hàng")?.Value;
                var sum = db.YeuThiches.Count(s=>s.MaKh==maKhachHang);
                return View("FavourtiesVM", new FavourtiesModelVM
            {
                quantity = sum
            });
            }
            else{
            return View("FavourtiesVM", new FavourtiesModelVM
            {
                quantity = 0
            });
            }
            }

    }
}