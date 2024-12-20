using Microsoft.AspNetCore.Mvc;
using VA_EcommerceWebsite.Data;
using VA_EcommerceWebsite.ViewModels;
namespace VA_EcommerceWebsite.ViewComponents
{
    [ViewComponent]
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly VAEcommerceContext db;
        public CategoriesViewComponent(VAEcommerceContext context)=> db=context;
        public IViewComponentResult Invoke(){
            var data= db.Loais.Select(lo=> new MenuLoaiVM{MaLoai = lo.MaLoai, TenLoai=lo.TenLoai, SoLuong=lo.HangHoas.Count}).OrderBy(p=>p.TenLoai);
            return View("CategoriesVM", data);
        }
    }
}