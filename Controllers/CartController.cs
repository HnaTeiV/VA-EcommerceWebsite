using Microsoft.AspNetCore.Mvc;
using VA_EcommerceWebsite.Data;
using VA_EcommerceWebsite.ViewModels;
using VA_EcommerceWebsite.Helpers;
using Microsoft.EntityFrameworkCore;
using VA_EcommerceWebsite.Mappers;
namespace VA_EcommerceWebsite.Controllers{
    public class CartController : Controller{

        private readonly VAEcommerceContext db;
        public CartController(VAEcommerceContext context){
            db = context;
        }
        public List<CartItemVM> Cart => HttpContext.Session.Get<List<CartItemVM>>(MySetting.CART_KEY) ?? new List<CartItemVM>();

        [HttpGet("Cart")]
        public IActionResult Index(){
            return View(Cart);
        }
        [HttpPost("/Cart/AddToCart/{id}")]
        public async Task<IActionResult> AddToCart(int id, int quantity=1){
            var gioHang=Cart;
            var item=gioHang.SingleOrDefault(p=> p.MaHh==id);
            if(item==null){
                var hangHoa = await db.HangHoas.SingleOrDefaultAsync(p => p.MaHh == id);
                if(hangHoa==null){
                    ViewData["Message"]= $"Không tìm thấy hàng hóa có mã {id}";
                    return View("NotFound");
                }
                else{
                    item = CartMappers.ToCartDto(hangHoa);
                    gioHang.Add(item);
                }
            }
            else{
                item.SoLuong+= quantity;
            }
            HttpContext.Session.Set(MySetting.CART_KEY, gioHang);
            return RedirectToAction("index");
        }
        [HttpGet("Cart/RemoveCart/{id}")]
        public IActionResult RemoveCart(int id)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.MaHh == id);
            if (item != null)
            {
                gioHang.Remove(item);
                HttpContext.Session.Set(MySetting.CART_KEY, gioHang);
            }
            
            return RedirectToAction("index");
        }
    }
}