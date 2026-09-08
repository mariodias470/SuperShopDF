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
            var products = _productRepository.GetAll();
            return Ok(products);
        }


    } // end class ProductsController

} // end namespace SuperShopDF.Web.Controllers.API
