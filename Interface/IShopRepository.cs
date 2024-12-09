using VA_EcommerceWebsite.ViewModels;

namespace VA_EcommerceWebsite.Interface{
    public interface IShopRepository{
        Task<List<HangHoaVM>> SortByPrice(priceFilterVM priceFilter);
    }
}