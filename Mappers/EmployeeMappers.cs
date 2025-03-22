using VA_EcommerceWebsite.Data;
using VA_EcommerceWebsite.ViewModels;

namespace VA_EcommerceWebsite.Mappers
{
    public static class EmployeeMappers
    {
        public static EmployeeVM ToEmployeeDto(this NhanVien nhanVienModel)
        {
            return new EmployeeVM
            {
                MaNv = nhanVienModel.MaNv,
                HoTen = nhanVienModel.HoTen,
                Email = nhanVienModel.Email,
                MatKhau = nhanVienModel.MatKhau,
                Sdt = nhanVienModel.Sdt,
                GioiTinh = nhanVienModel.GioiTinh,
                MaPb = nhanVienModel.MaPb,
                ChucVu = nhanVienModel.ChucVu,
                NgayVaoLam = nhanVienModel.NgayVaoLam,
                Luong = nhanVienModel.Luong,
                IsActive = nhanVienModel.IsActive
            };
        }
    }

}