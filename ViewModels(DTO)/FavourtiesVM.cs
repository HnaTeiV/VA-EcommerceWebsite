using System.ComponentModel.DataAnnotations;

namespace VA_EcommerceWebsite.ViewModels{
    public class FavourtiesVM{
        [Key]
        public int MaYt {get; set;}
        public int MaHh{ get;set;}
        public string MaKh{get;set;}
        public DateTime NgayChon{get;set;}
        public string MoTa{get;set;}
    }
    public class FavourtiesModelVM{
        public int quantity {get;set;}
    }
}