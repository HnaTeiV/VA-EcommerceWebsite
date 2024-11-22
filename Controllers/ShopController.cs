using Microsoft.AspNetCore.Mvc;
using VA_EcommerceWebsite.Data;
using VA_EcommerceWebsite.Interface;
using VA_EcommerceWebsite.Repository;
using VA_EcommerceWebsite.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList.Extensions;
using X.PagedList;
namespace VA_EcommerceWebsite.Controllers{
    public class ShopController: Controller{
        private readonly VAEcommerceContext db;
        private readonly IHangHoaRepository _hangHoaRepo;
        private readonly IShopRepository _shopRepo;
        private IPagedList<HangHoaVM> result ;
        public ShopController(VAEcommerceContext context, IHangHoaRepository hangHoaRepository, IShopRepository shopRepository){
            db=context;
            _hangHoaRepo=hangHoaRepository;
            _shopRepo=shopRepository;
        }
        [HttpGet("Shop/{loai?}")]
        public async Task<IActionResult> Index(int? loai,int? page){
            var pageSize=9;
            var pageNumber= page?? 1;
            var result = await _hangHoaRepo.GetAllAsync(loai);
            var pagedResult= result.AsQueryable().ToPagedList(pageNumber,pageSize);
            return View(pagedResult);
        }

        
        [HttpPost("Shop")]
        public async Task<IActionResult> SortByPrice(int min, int max,int? page){
            var pageSize=9;
            var pageNumber= page??1;
            var priceFilter = new priceFilterVM { MinPrice = min, MaxPrice = max };
            var res = await _shopRepo.SortByPrice(priceFilter);
            var pagedResult=res.AsQueryable().ToPagedList(pageNumber,pageSize);
            result = pagedResult;
            return PartialView("_ProductList", result);
        }

        [HttpPost("Shop/Search")]
        public async Task<IActionResult> Search(string? query,int? page){
            var pageSize=9;
            var pageNumber= page??1;
            var res = await _hangHoaRepo.SearchAsync(query);
            var pagedResult=res.AsQueryable().ToPagedList(pageNumber,pageSize);
            result = pagedResult;
            return PartialView("_ProductList", result);
        }
    }
}