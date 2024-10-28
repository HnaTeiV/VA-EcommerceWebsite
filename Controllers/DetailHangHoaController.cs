using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VA_EcommerceWebsite.Data;
using VA_EcommerceWebsite.ViewModels;
using System.Collections.Generic;
using VA_EcommerceWebsite.Mappers;

namespace VA_EcommerceWebsite.Controllers
{
    public class DetailHangHoaController : Controller
    {

        private readonly VAEcommerceContext db;
        public DetailHangHoaController(VAEcommerceContext context) => db = context;
        [HttpGet("DetailHangHoa/{id?}")]
        public IActionResult Index(int? id)
        {
           var result=db.HangHoas.FirstOrDefault(p=>p.MaHh==id.Value);
           if(result==null) return View("NotFound");
            result.SoLanXem += 1;
           db.SaveChanges();
           var hangHoaItem=result.ToDetailHangHoaDto();

           List<HangHoaVM> hanghoaList = db.HangHoas.Where(p=>p.MaLoai==hangHoaItem.MaLoai).Select(p=> p.ToDetailHangHoaDto()).ToList();
            var combineObject= new ObjectAndListObjectHangHoa{
                HangHoaItem= hangHoaItem,
                HangHoaList= hanghoaList
            };

           return View(combineObject);
        }
    }
}
