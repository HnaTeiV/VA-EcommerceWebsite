using VA_EcommerceWebsite.Data;
using VA_EcommerceWebsite.ViewModels;
namespace VA_EcommerceWebsite.Mappers
{
    public static class CartMappers
    {
        public static CartItemVM ToCartDto(this HangHoa ItemModel)
        {
            return new CartItemVM
            {
                MaHh = ItemModel.MaHh,
                TenHh = ItemModel.TenHh,
                DonGia = ItemModel.DonGia ?? 0.0,
                Hinh = ItemModel.Hinh ?? string.Empty,
                SoLuong = 1
            };
        }
    }
}