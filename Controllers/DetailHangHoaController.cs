using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VA_EcommerceWebsite.Data;
using VA_EcommerceWebsite.ViewModels;
using System.Collections.Generic;

namespace VA_EcommerceWebsite.Controllers
{
    public class DetailHangHoaController : Controller
    {
        private readonly int[] LoaiArray = {1000,1001,1002,1003,1004,1005,1006,1007};
        private readonly VAEcommerceContext db;
        public DetailHangHoaController(VAEcommerceContext context) => db = context;
        public IActionResult GetDetailHangHoa(int? id)
        {
           var result=db.HangHoas.FirstOrDefault(p=>p.MaHh==id.Value);
           if(result==null) return View("NotFound");
            result.SoLanXem += 1;
           db.SaveChanges();
           var hangHoaItem=new HangHoaVM{
            MaHh = result.MaHh,
            TenHh = result.TenHh,
            DonGia = result.DonGia ?? 0,
            Hinh = result.Hinh ?? "",
            MoTaNgan = result.MoTaDonVi ?? "",
            MaLoai = result.MaLoai,
            MoTa=result.MoTa,
            LuotXem=result.SoLanXem
           };

           List<HangHoaVM> hanghoaList = db.HangHoas.Where(p=>p.MaLoai==hangHoaItem.MaLoai).Select(p=> new HangHoaVM{
             MaHh = p.MaHh,
            TenHh = p.TenHh,
            DonGia = p.DonGia ?? 0,
            Hinh = p.Hinh ?? "",
            MoTaNgan = p.MoTaDonVi ?? "",
            MaLoai = p.MaLoai,
            MoTa=p.MoTa,
            LuotXem=p.SoLanXem
           }).ToList();
            var combineObject= new ObjectAndListObjectHangHoa{
                HangHoaItem= hangHoaItem,
                HangHoaList= hanghoaList
            };

           return View(combineObject);
        }
    }
}
