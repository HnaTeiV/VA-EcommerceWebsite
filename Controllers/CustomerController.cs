using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using VA_EcommerceWebsite.Data;
using VA_EcommerceWebsite.Helpers;
using VA_EcommerceWebsite.ViewModels;

namespace VA_EcommerceWebsite.Controllers{
    public class CustomerController: Controller{
        private readonly VAEcommerceContext db;
        private readonly IMapper _mapper;
        public CustomerController(VAEcommerceContext context, IMapper mapper){
            db=context;
            _mapper=mapper;
        }
        public IActionResult Register(RegisterVM model){
            if(ModelState.IsValid){
                var khachHang=_mapper.Map<KhachHang>(model);
                khachHang.RandomKey=Util.GenerateRandomKey();
                khachHang.MatKhau=model.MatKhau.ToMd5Hash(khachHang.MatKhau);
                khachHang.HieuLuc=true;
                khachHang.VaiTro=0;
                
            }
            return View();
        }
}}