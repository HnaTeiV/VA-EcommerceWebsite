using Microsoft.AspNetCore.Mvc;
using VA_EcommerceWebsite.Data;
using VA_EcommerceWebsite.ViewModels;

namespace VA_EcommerceWebsite.Interface{
    public interface ICustomerRepository{

        KhachHang Register(RegisterVM model, IFormFile Hinh);
        Task<KhachHang> Login(LoginVM model);
    }
}