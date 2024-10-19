using Microsoft.AspNetCore.Mvc;
using VA_EcommerceWebsite.ViewModels;

namespace VA_EcommerceWebsite.ViewComponents
{
    public class CarouselViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var headerViewModel =
                 new List<CarouselItemVM>
                {
                    new CarouselItemVM
                    {
                        IsActive = true,
                        ImageFileName = "carousel-1.jpg",
                        Subtitle = "10% Off Your First Order",
                        Title = "Fashionable Dress",
                        ButtonText = "Shop Now",
                        ButtonUrl = "/shop"
                    },
                    new CarouselItemVM
                    {
                        IsActive = false,
                        ImageFileName = "carousel-2.jpg",
                        Subtitle = "Spring Collection",
                        Title = "Beautiful Blouse",
                        ButtonText = "Shop Now",
                        ButtonUrl = "/shop"
                    }
                };
                  return View("CarouselVM", headerViewModel);
        }

          
    }
}
