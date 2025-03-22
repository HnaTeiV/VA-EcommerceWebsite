using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VA_EcommerceWebsite.Data;
using VA_EcommerceWebsite.Interface;
using VA_EcommerceWebsite.ViewModels;

namespace VA_EcommerceWebsite.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly VAEcommerceContext db;
        private readonly IMapper _mapper;
        public EmployeeRepository(VAEcommerceContext context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }
        public async Task<string> AddProduct(ProductsManageVM model)
        {
            HangHoa product = _mapper.Map<HangHoa>(model);
            if (product == null)
            {
                return "Have a issue in progress map model";
            }
            else
            {
                product.NgaySx = DateTime.UtcNow;
                product.SoLanXem = 0;
                await db.AddAsync(product);
                db.SaveChanges();
                return "Add product success";
            }

        }
        public async Task<string> UpdateProduct(ProductsManageVM model){
              var product = await db.HangHoas.FirstOrDefaultAsync(p => p.MaHh == model.MaHh);

            if (product == null)
            {
               return "Product not found." ;
            }

            // Ensure EF is tracking the entity correctly
            if (db.Entry(product).State == EntityState.Detached)
            {
                db.Attach(product);
            }

            // Update necessary fields
            product.TenHh = model.TenHh;
            product.MaLoai = (int)model.MaLoai;
            product.MoTaDonVi = model.MoTaDonVi;
            product.DonGia = model.DonGia;
            product.NgaySx = (DateTime)model.NgaySX;
            product.GiamGia = (double)model.GiamGia;
            product.SoLanXem = (int)model.SoLanXem;
            product.MaNcc = model.MaNCC;
            product.TenAlias = model.TenAlias;
            product.Hinh = model.Hinh;
            product.MoTa = model.MoTa;

            try
            {
                int rowsAffected = db.SaveChanges();

                if (rowsAffected == 0)
                {
                    return  "No changes were made.";
                }

                return "Product updated successfully!";
            }
            catch (DbUpdateException ex)
            {
                return $"Error: {ex.InnerException?.Message ?? ex.Message}";
            }
        }

    }
}