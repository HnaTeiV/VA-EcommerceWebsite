using VA_EcommerceWebsite.Data;
using VA_EcommerceWebsite.ViewModels;

namespace VA_EcommerceWebsite.Interface{
    public interface IEmployeeRepository{
        Task<string> AddProduct(ProductsManageVM model);
        Task<string> UpdateProduct(ProductsManageVM model);
    }
}