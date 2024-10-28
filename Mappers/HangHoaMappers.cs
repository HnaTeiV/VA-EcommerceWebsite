using VA_EcommerceWebsite.Data;
using VA_EcommerceWebsite.Models;
using VA_EcommerceWebsite.ViewModels; // Ensure you have the correct namespace for your HangHoa model

namespace VA_EcommerceWebsite.Mappers{
    public static class HangHoaMappers{
        public static HangHoaVM ToHangHoaDto(this HangHoa hangHoaModel){
            return new HangHoaVM{
                MaHh=hangHoaModel.MaHh,
                MaLoai=hangHoaModel.MaLoai,
                TenHh=hangHoaModel.TenHh,
                DonGia=hangHoaModel.DonGia ?? 0, // Provide a default value for nullable DonGia
                Hinh=hangHoaModel.Hinh ?? "",
                MoTaNgan=hangHoaModel.MoTaDonVi ?? "",
                MoTa=hangHoaModel.MoTa,
                MaLoaiNavigation = hangHoaModel.MaLoaiNavigation?.TenLoai ?? "", // Use null-conditional operator
                LuotXem=hangHoaModel.SoLanXem
            };
        }
        public static HangHoaVM ToSearchHangHoaDto(this HangHoa hangHoaModel){
            return new HangHoaVM{
                 MaHh=hangHoaModel.MaHh,
                TenHh=hangHoaModel.TenHh,
                DonGia=hangHoaModel.DonGia ?? 0,
                Hinh=hangHoaModel.Hinh ?? "",
                MoTaNgan=hangHoaModel.MoTaDonVi ?? "",
                MaLoaiNavigation=hangHoaModel.MaLoaiNavigation?.TenLoai ?? "" // Use null-conditional operator
            };
        }
    }
}
