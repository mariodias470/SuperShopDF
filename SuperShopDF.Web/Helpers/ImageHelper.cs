using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SuperShopDF.Web.Helpers
{
    public class ImageHelper : IImageHelper
    {
        public async Task<string> UploadImageAsync(IFormFile imageFile, string folder)
        {
            string guid = Guid.NewGuid().ToString();
            string file = $"{guid}.jpg";

            // Process the uploaded image file
            string path = Path.Combine
                                (
                                    Directory.GetCurrentDirectory(),
                                    $"wwwroot\\images\\{folder}",
                                    // FORA model.ImageFile.FileName
                                    file
                                );

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);

            }

            return $"~/images/{folder}/{/*model.ImageFile.FileName*/ file}";
        } // end UploadImageAsync()


    } // end class ImageHelper
} // end namespace SuperShopDF.Web.Helpers
