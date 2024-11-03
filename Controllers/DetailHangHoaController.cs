using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VA_EcommerceWebsite.Data;
using VA_EcommerceWebsite.ViewModels;
using System.Collections.Generic;
using VA_EcommerceWebsite.Mappers;
using VA_EcommerceWebsite.Interface;

namespace VA_EcommerceWebsite.Controllers
{
    public class DetailHangHoaController : Controller
    {

        private readonly VAEcommerceContext db;
        private readonly IHangHoaRepository _hangHoaRepo;
        public DetailHangHoaController(VAEcommerceContext context,IHangHoaRepository hangHoaRepository){
            _hangHoaRepo=hangHoaRepository;
            db = context;
        } 
        [HttpGet("DetailHangHoa/{id?}")]
        public async Task<IActionResult> Index(int? id)
        {
            if(id.HasValue)
            {
                var hangHoaItem =await _hangHoaRepo.GetHangHoaByIdAsync(id.Value);
                List<HangHoaVM> hanghoaList = db.HangHoas.Where(p => p.MaLoai == hangHoaItem.MaLoai).Select(p => p.ToDetailHangHoaDto()).ToList();
                var combineObject = new ObjectAndListObjectHangHoa{
                    HangHoaItem = hangHoaItem,
                    HangHoaList= hanghoaList
                };
                return View(combineObject);
            }
            else{
                return View("NotFound");
            }
           
        }
    }
}
