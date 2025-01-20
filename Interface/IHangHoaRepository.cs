using VA_EcommerceWebsite.Data;
using VA_EcommerceWebsite.ViewModels;

namespace VA_EcommerceWebsite.Interface
{
    public interface IHangHoaRepository
    {
        Task<HangHoaVM> GetHangHoaByIdAsync(int id);
        Task<List<HangHoaVM>> GetAllAsync(int ?loai);
        Task<List<HangHoaVM>> SearchAsync(string? query);
        

    }
}