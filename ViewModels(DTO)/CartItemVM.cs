namespace VA_EcommerceWebsite.ViewModels{
    public class CartItemVM{
        public int MaHh{get;set;}
        public string Hinh{get;set;}
        public string TenHh{get;set;}
        public double? DonGia{get;set;}
        public int SoLuong{get;set;}
        public double ThanhTien => (double)(SoLuong * DonGia);
    }

    }