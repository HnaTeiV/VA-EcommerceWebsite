using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using VA_EcommerceWebsite.Data;
using VA_EcommerceWebsite.ViewModels;
using VA_EcommerceWebsite.Mappers;
using VA_EcommerceWebsite.Interface;

namespace VA_EcommerceWebsite.Controllers
{
    public class HangHoaController:Controller
    {
        private readonly VAEcommerceContext db;
        private readonly IHangHoaRepository _hangHoaRepo;
        public HangHoaController(VAEcommerceContext context,IHangHoaRepository hangHoaRepository) {
            _hangHoaRepo= hangHoaRepository;
            db=context;
        } 

        [HttpGet("{loai?}")]
        public async Task<IActionResult> Index(int? loai){
            var result = await _hangHoaRepo.GetAllAsync(loai);
            return View(result);
        }
        [HttpGet("HangHoa/Search")]
        [HttpPost("HangHoa/Search")]
        public async Task<IActionResult> Search(string? query){
            var result = await _hangHoaRepo.SearchAsync(query);

            return View(result);
        }
    }
}