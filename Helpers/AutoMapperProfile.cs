using AutoMapper;
using Microsoft.Build.Framework.Profiler;
using VA_EcommerceWebsite.Data;
using VA_EcommerceWebsite.ViewModels;

namespace VA_EcommerceWebsite.Helpers{
    public class AutoMapperProfile: Profile{
        public AutoMapperProfile(){
            CreateMap<RegisterVM, KhachHang>();
            CreateMap<ProductsManageVM,HangHoa>();
        }
    }
}