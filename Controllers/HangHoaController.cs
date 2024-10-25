using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using VA_EcommerceWebsite.Data;
using VA_EcommerceWebsite.ViewModels;
namespace VA_EcommerceWebsite.Controllers
{
    public class HangHoaController:Controller
    {
        private readonly VAEcommerceContext db;
        public HangHoaController(VAEcommerceContext context)=> db=context;

        public IActionResult Index(int? loai){
            var hangHoas=db.HangHoas.AsQueryable();
            if(loai.HasValue)
            {
                hangHoas=hangHoas.Where(p=>p.MaLoai==loai.Value);
            }
            var result=hangHoas.Select(p=> new HangHoaVM{
                MaHh=p.MaHh,
                TenHh=p.TenHh,
                DonGia=p.DonGia ?? 0,
                Hinh=p.Hinh??"",
                MoTaNgan=p.MoTaDonVi??"",
                MaLoaiNavigation=p.MaLoaiNavigation.TenLoai,
                LuotXem=p.SoLanXem
            });
            return View(result);
        }
        public IActionResult Search(string? query){
            var hangHoas=db.HangHoas.AsQueryable();

            if(query !=null)
            {
                hangHoas=hangHoas.Where(p=> p.TenHh.Contains(query));
            }
            var result= hangHoas.Select(p=> new HangHoaVM{
                 MaHh=p.MaHh,
                TenHh=p.TenHh,
                DonGia=p.DonGia ?? 0,
                Hinh=p.Hinh??"",
                MoTaNgan=p.MoTaDonVi??"",
                MaLoaiNavigation=p.MaLoaiNavigation.TenLoai
            });

            return View(result);
        }
    }
}