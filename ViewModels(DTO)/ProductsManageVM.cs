namespace VA_EcommerceWebsite.ViewModels
{
    public class ProductsManageVM
    {
        public int MaHh { get; set; }
        public string TenHh { get; set; }
        public int MaLoai { get; set; }
        public string MoTaDonVi { get; set; }
        public double DonGia { get; set; }
        public DateTime NgaySX { get; set; } // Manufacture Date
        public double GiamGia { get; set; }  // Discount
        public int SoLanXem { get; set; }
        public string MaNCC { get; set; } // Supplier ID
        public string TenAlias { get; set; } // Alias Name
        public string Hinh { get; set; }
        
        public string MoTa { get; set; }

        
    }

}