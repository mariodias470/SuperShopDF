using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SuperShopDF.Web.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile, string folder);
    } // end interface IImageHelper
} // end namespace SuperShopDF.Web.Helpers
