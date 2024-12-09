using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using VA_EcommerceWebsite.Helpers;
using VA_EcommerceWebsite.ViewModels;

namespace VA_EcommerceWebsite.ViewComponents
{
    [ViewComponent]
    public class CartViewComponent : ViewComponent
    {
        public List<CartItemVM> Cart => HttpContext.Session.Get<List<CartItemVM>>(MySetting.CART_KEY) ?? new List<CartItemVM>();

        public IViewComponentResult Invoke()
        {
            var giohang = Cart;
            return View("Cart", new CartModel
            {
                Quantity = giohang.Sum(p => p.SoLuong)
            });
        }
    }
}