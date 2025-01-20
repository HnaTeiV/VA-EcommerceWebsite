using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VA_EcommerceWebsite.Data;
using VA_EcommerceWebsite.ViewModels;
using System.Collections.Generic;
using VA_EcommerceWebsite.Mappers;
using VA_EcommerceWebsite.Interface;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;

namespace VA_EcommerceWebsite.Controllers
{
    public class DetailHangHoaController : Controller
    {

        private readonly VAEcommerceContext db;
        private readonly IHangHoaRepository _hangHoaRepo;
        public DetailHangHoaController(VAEcommerceContext context, IHangHoaRepository hangHoaRepository)
        {
            _hangHoaRepo = hangHoaRepository;
            db = context;
        }
        [HttpGet("DetailHangHoa/{id?}")]
        public async Task<IActionResult> Index(int? id)
        {
            if (id.HasValue)
            {
                var hangHoaItem = await _hangHoaRepo.GetHangHoaByIdAsync(id.Value);
                List<HangHoaVM> hanghoaList = db.HangHoas.Where(p => p.MaLoai == hangHoaItem.MaLoai).Select(p => p.ToDetailHangHoaDto()).ToList();
                var combineObject = new ObjectAndListObjectHangHoa
                {
                    HangHoaItem = hangHoaItem,
                    HangHoaList = hanghoaList
                };
                return View(combineObject);
            }
            else
            {
                return View("NotFound");
            }

        }

        [HttpPost("DetailHangHoa/AddFavourties/{MaHh}")]
        public IActionResult AddFavourties(int maHh)
        {
            // Ensure the user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                var maKhachHang = User?.Claims.FirstOrDefault(c => c.Type == "Mã khách hàng")?.Value;
                // Create a new Favourite object (assuming you have such a class)
                var favourite = new YeuThich
                {
                    MaKh = maKhachHang,
                    MaHh = maHh,
                    NgayChon = DateTime.UtcNow,
                    MoTa = ""
                };

                // Add the favourite object to the YeuThich table
                db.YeuThiches.Add(favourite);

                // Save changes to the database
                db.SaveChanges();

                
                

                return Json(new { success = true, message = "Add to Favourites success" });
            }
            else
            {
                // If the user is not authenticated
                return Json(new { success = false, message = "User is not authenticated." });
            }
        }
    }
}
