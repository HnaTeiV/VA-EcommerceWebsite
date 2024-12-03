using System.Security.Cryptography;
using System.Text;

namespace VA_EcommerceWebsite.Helpers{
    public static class DataEncryptionExtensions{
        public static string ToMd5Hash(this string Password, string? saltKey){
            using(var md5=MD5.Create()){
                byte[] data=md5.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(Password,saltKey)));
                StringBuilder sBuilder= new StringBuilder();
                for(int i=0;i<data.Length;i++){
                    sBuilder.Append(data[i].ToString("x2"));

                }
                return sBuilder.ToString();
            }
        }
    }
}