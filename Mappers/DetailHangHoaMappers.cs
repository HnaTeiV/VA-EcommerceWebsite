using VA_EcommerceWebsite.Data;
using VA_EcommerceWebsite.ViewModels;

namespace VA_EcommerceWebsite.Mappers{
    public static class DetailHangHoaMappers{
        public static HangHoaVM ToDetailHangHoaDto(this HangHoa DetailHangHoaModel){
            return new HangHoaVM{
                 MaHh = DetailHangHoaModel.MaHh,
                TenHh = DetailHangHoaModel.TenHh,
                DonGia = DetailHangHoaModel.DonGia ?? 0,
                Hinh = DetailHangHoaModel.Hinh ?? "",
                MoTaNgan = DetailHangHoaModel.MoTaDonVi ?? "",
                MaLoai = DetailHangHoaModel.MaLoai,
                MoTa=DetailHangHoaModel.MoTa,
                LuotXem=DetailHangHoaModel.SoLanXem
            };
        }
    }
}