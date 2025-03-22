using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VA_EcommerceWebsite.Data;
using VA_EcommerceWebsite.Interface;
using VA_EcommerceWebsite.ViewModels;
using VA_EcommerceWebsite.Helpers;
using Microsoft.EntityFrameworkCore;
namespace VA_EcommerceWebsite.Repository{
    public class CustomerRepository : ICustomerRepository{
        private readonly VAEcommerceContext db;
        private readonly IMapper _mapper;
        public CustomerRepository(VAEcommerceContext context, IMapper mapper){
            db=context;
            _mapper=mapper;
        }
        public KhachHang Register(RegisterVM model,IFormFile Hinh){
             var khachHang = _mapper.Map<KhachHang>(model);
                    khachHang.RandomKey = Util.GenerateRandomKey();
                    khachHang.MatKhau = model.MatKhau.ToMd5Hash(khachHang.RandomKey);
                    khachHang.HieuLuc = true;
                    khachHang.VaiTro = 0;
                    if (Hinh != null)
                    {
                        khachHang.Hinh = Util.UploadImage(Hinh, "KhachHang");
                    }
            return khachHang;
        }
        public async Task<KhachHang> Login(LoginVM model){
            var khachHang= await db.KhachHangs.SingleOrDefaultAsync(kh=>kh.MaKh==model.TaiKhoan);
            if(khachHang==null){
                return null;
            }else{
                var hashPassword= model.MatKhau.ToMd5Hash(khachHang.RandomKey);
                if(khachHang.HieuLuc && khachHang.MatKhau==hashPassword){
                    return khachHang;
                }
            }
            return null;
        }
    }
}