using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using SuperShopDF.Web.Data.Entities;

namespace SuperShopDF.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    // public class ProductsController: Microsoft.AspNetCore.Mvc.Controller
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            // FORA - var products = _productRepository.GetAll(); 
            // 1.13.30 vídeo ASP.NET_MVC_11:
            
            return Ok(_productRepository.GetAllWithUsers());
        }


        // 1.16.30 vídeo ASP.NET_MVC_11:
        // A partir de agora, se precisarmos de fazer alteração procedemos do seguinte modo:
        //     1º) Ir ao Interface e pôr aí um método novo.
        //     2º) Ir à classe do repositório e implementar o respectivo método.
        //     3º) Ir ao controlador.
        // ... e, se ainda fôr necessa´rio, vamos à View

    } // end class ProductsController

} // end namespace SuperShopDF.Web.Controllers.API
