using Microsoft.EntityFrameworkCore;
using VA_EcommerceWebsite.Data;
using VA_EcommerceWebsite.Interface;
using VA_EcommerceWebsite.Mappers;
using VA_EcommerceWebsite.ViewModels;

namespace VA_EcommerceWebsite.Repository
{
    public class ShopRepositoy : IShopRepository
    {
        private readonly VAEcommerceContext db;
        public ShopRepositoy(VAEcommerceContext context) => db = context;

        public async Task<List<HangHoaVM>> SortByPrice(priceFilterVM priceFilter) {
            var hangHoa = await db.HangHoas.ToListAsync();

            var result = hangHoa.Where(p => priceFilter.MinPrice <= p.DonGia && p.DonGia <= priceFilter.MaxPrice).ToList();
            List<HangHoaVM> res = result.Select(p => p.ToHangHoaDto()).ToList();
            return res;
        }
    }
}