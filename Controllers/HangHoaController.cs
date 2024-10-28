using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using VA_EcommerceWebsite.Data;
using VA_EcommerceWebsite.ViewModels;
using VA_EcommerceWebsite.Mappers;

namespace VA_EcommerceWebsite.Controllers
{
    public class HangHoaController:Controller
    {
        private readonly VAEcommerceContext db;
        public HangHoaController(VAEcommerceContext context)=> db=context;
        [HttpGet("{loai?}")]
        public IActionResult Index(int? loai){
            var hangHoas=db.HangHoas.AsQueryable();
            if(loai.HasValue)
            {
                hangHoas=hangHoas.Where(p=>p.MaLoai==loai.Value);
            }
            var result=hangHoas.Select(p=> p.ToHangHoaDto());
            return View(result);
        }
        [HttpGet("HangHoa/Search")]
        [HttpPost("HangHoa/Search")]
        public IActionResult Search(string? query){
            var hangHoas=db.HangHoas.AsQueryable();

            if(query != null)
            {
                hangHoas=hangHoas.Where(p=> p.TenHh.Contains(query));
            }
            var result= hangHoas.Select(p=> p.ToSearchHangHoaDto());

            return View(result);
        }
    }
}