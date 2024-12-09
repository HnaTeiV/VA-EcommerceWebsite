using System.Text;

namespace VA_EcommerceWebsite.Helpers
{
    public class Util
    {
        public static string GenerateRandomKey(int length = 5)
        {
            var pattern = @"qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
            var sb = new StringBuilder();
            var rd = new Random();
            for (int i = 0; i < length; i++)
            {
                sb.Append(pattern[rd.Next(0, pattern.Length)]);

            }
            return sb.ToString();
        }
        public static string UploadImage(IFormFile Hinh, string folder)
        {
            try
            {
                if (Hinh == null || Hinh.Length == 0)
                {
                    return string.Empty;
                }

                // Sanitize file name to prevent directory traversal
                var fileName = Path.GetFileName(Hinh.FileName);

                // Ensure the folder exists
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", folder);
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                // Set the full path for saving
                var fullPath = Path.Combine(savePath, fileName);

                // Avoid overwriting existing files by appending a GUID if needed
                if (File.Exists(fullPath))
                {
                    var uniqueSuffix = Guid.NewGuid().ToString();
                    fileName = $"{Path.GetFileNameWithoutExtension(Hinh.FileName)}_{uniqueSuffix}{Path.GetExtension(Hinh.FileName)}";
                    fullPath = Path.Combine(savePath, fileName);
                }

                using (var myfile = new FileStream(fullPath, FileMode.Create))
                {
                    Hinh.CopyTo(myfile);
                }

                return fileName;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism could be added here)
                Console.WriteLine($"Error: {ex.Message}");
                return string.Empty;
            }
        }

    }
}