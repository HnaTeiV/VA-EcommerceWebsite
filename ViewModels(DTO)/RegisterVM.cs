using System.ComponentModel.DataAnnotations;
namespace VA_EcommerceWebsite.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc.")]
        [StringLength(20, ErrorMessage = "Tên đăng nhập phải từ 4 đến 20 ký tự.", MinimumLength = 4)]
        public string MaKh { get; set; } = null!;

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Mật khẩu phải từ 8 đến 50 ký tự.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; } = null!;

        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Họ tên là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Họ tên phải từ 8 đến 50 ký tự.", MinimumLength = 8)]
        public string HoTen { get; set; } = null!;

        [Display(Name = "Giới tính")]
        [Required(ErrorMessage = "Giới tính là bắt buộc.")]
        public bool GioiTinh { get; set; } = true;

        [Display(Name = "Ngày sinh")]
        [Required(ErrorMessage = "Ngày sinh là bắt buộc.")]
        public DateTime NgaySinh { get; set; }

        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Địa chỉ là bắt buộc.")]
        [StringLength(60, ErrorMessage = "Địa chỉ phải từ 8 đến 60 ký tự.", MinimumLength = 8)]
        public string DiaChi { get; set; } = null!;

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
        [RegularExpression(@"0[98765]\d{8}", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string DienThoai { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; } = null!;

        [Display(Name = "Hình đại diện")]
        public string? Hinh { get; set; }

        public bool HieuLuc { get; set; } = false;
    }
}
