using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using VA_EcommerceWebsite.Data;
using VA_EcommerceWebsite.Interface;
using VA_EcommerceWebsite.Mappers;

using VA_EcommerceWebsite.ViewModels;

namespace VA_EcommerceWebsite.Repository
{
    public class HangHoaRepository : IHangHoaRepository
    {
        private readonly VAEcommerceContext db;
        
        public HangHoaRepository(VAEcommerceContext context) =>db=context;
            
        
        public async Task<List<HangHoaVM>> GetAllAsync(int? loai)
        {
            var result = await db.HangHoas.ToListAsync();
            if(loai.HasValue){
                result = result.Where(p => p.MaLoai == loai.Value).ToList();
            }
            var hangHoaVMs = result.Select(p => p.ToHangHoaDto()).ToList();
            return hangHoaVMs;
        }

        public async Task<HangHoaVM> GetHangHoaByIdAsync(int id)
        {
           var result = await db.HangHoas.FirstOrDefaultAsync(p => p.MaHh == id);

            if (result != null)
            {
                result.SoLanXem += 1;
                await db.SaveChangesAsync();
            }
           return result?.ToHangHoaDto();
        }


        public async Task<List<HangHoaVM>> SearchAsync(string? query)
        {
            var hangHoas= await db.HangHoas.ToListAsync();
            if(query != null)
            {
                hangHoas=(List<HangHoa>)hangHoas.Where(p=> p.TenHh.Contains(query));
            }
            var result= hangHoas.Select(p=> p.ToSearchHangHoaDto()).ToList();
            return result;
        }

    }
}