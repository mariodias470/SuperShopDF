using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using SuperShopDF.Web.Data.Entities;


// Ficheiro criado no início do vídeo ASP.NET_MVC_11.

namespace SuperShopDF.Web.Models
{
    public class ProductViewModel : Product
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }


    } // end class ProductViewModel
} // end namespace SuperShopDF.Web.Models
