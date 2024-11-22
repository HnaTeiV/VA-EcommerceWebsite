using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using VA_EcommerceWebsite.Data;
using VA_EcommerceWebsite.ViewModels;
using VA_EcommerceWebsite.Mappers;
using VA_EcommerceWebsite.Interface;
using X.PagedList;
using X.PagedList.Mvc;
using X.PagedList.Extensions;
namespace VA_EcommerceWebsite.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly VAEcommerceContext db;
        private readonly IHangHoaRepository _hangHoaRepo;
        public HangHoaController(VAEcommerceContext context, IHangHoaRepository hangHoaRepository)
        {
            _hangHoaRepo = hangHoaRepository;
            db = context;
        }

        [HttpGet("HangHoa/{loai?}")]
        public async Task<IActionResult> Index(int? loai, int? page)
        {
            int pageSize = 12;
            int pageNumber = page ?? 1;
            ViewData["CurrentLoai"] = loai;

            // Fetch data from the repository
            var hangHoas = await _hangHoaRepo.GetAllAsync(loai);

            // Use X.PagedList for pagination
            var pagedResult = hangHoas.AsQueryable().ToPagedList(pageNumber, pageSize);

            return View(pagedResult);
        }

        [HttpGet("HangHoa/Search")]
        [HttpPost("HangHoa/Search")]
        public async Task<IActionResult> Search(string? query,int? page)
        {
            var pageSize=12;
            var pageNumber= page?? 1;

            var result = await _hangHoaRepo.SearchAsync(query);
            var pagedResult=result.AsQueryable().ToPagedList(pageNumber,pageSize);
            return View(pagedResult);
        }
    }
}