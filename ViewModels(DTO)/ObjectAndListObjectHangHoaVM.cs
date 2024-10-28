using System.Collections.Generic;
namespace VA_EcommerceWebsite.ViewModels{
    public class ObjectAndListObjectHangHoa{
        public HangHoaVM? HangHoaItem{get;set;}
        public List<HangHoaVM> HangHoaList{get;set;}
        public ObjectAndListObjectHangHoa(){
            HangHoaList= new List<HangHoaVM>();
        }
    }
}