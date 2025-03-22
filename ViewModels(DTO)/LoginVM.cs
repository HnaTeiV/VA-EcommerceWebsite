using System.ComponentModel.DataAnnotations;

namespace VA_EcommerceWebsite.ViewModels{
    public class LoginVM(){

        [Display(Name ="Tên người dùng")]
        
        public string TaiKhoan{get;set;}

        [Display(Name ="Mật khẩu")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; } = null!;
    }
}