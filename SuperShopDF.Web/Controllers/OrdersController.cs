using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperShopDF.Web.Data;
using SuperShopDF.Web.Data.Entities;
using SuperShopDF.Web.Models;


namespace SuperShopDF.Web.Controllers
{   /*
        Index()
        Create()
        AddProduct()
     */

    // 58.43 - vídeo ASP-NET_MVC_22
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository; // 25.36 vídeo ASP-NET_MVC_23: 

        // FORA 25.24 vídeo ASP-NET_MVC_23: public OrdersController(IOrderRepository orderRepository)
        public OrdersController(IOrderRepository orderRepository, IProductRepository productRepository) // Injectar o Product Repository e criar o filed
        {                                                                                               // 25.24 vídeo ASP-NET_MVC_23: 
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _orderRepository.GetOrderAsync(this.User.Identity.Name);
            // return View();   // ERRO --> NOTA BENE:
            // esqueci-me de passar o modelo para dentro da View
            /* ---------------------------------------------
                Exception thrown, at line 42 from /SuperShopDF.Web.Data.Entities.Order/Index.chtml:
                System.NullReferenceException: 'Object reference not set to an instance of an object.'
                
                SuperShopDF.Web.Views.dll!AspNetCore.Views_Orders_Index.ExecuteAsync() Line 42
                	at D:\CppCet105\RS2026\Projs\v78\SuperShopDF\SuperShopDF.Web\Views\Orders\Index.cshtml(42)
              ---------------------------------------------*/

                                // 1.01.09 vídeo ASP-NET_MVC_22
                                // 1.01.09 vídeo ASP-NET_MVC_22
            return View(model); // 1.01.09 vídeo ASP-NET_MVC_22
        }


        // 06.48 - vídeo ASP-NET_MVC_23:
        public async Task<IActionResult> Create()
        {
            var model = await _orderRepository.GetDetailsTempsAsync(this.User.Identity.Name);
            // return View();   // ERRO --> NOTA BENE:
            // esqueci-me de passar o modelo para dentro da View
            /* ---------------------------------------------
                Exception thrown, at line 42 from /SuperShopDF.Web.Data.Entities.Order/Index.chtml:
                System.NullReferenceException: 'Object reference not set to an instance of an object.'
                
                SuperShopDF.Web.Views.dll!AspNetCore.Views_Orders_Index.ExecuteAsync() Line 42
                	at D:\CppCet105\RS2026\Projs\v78\SuperShopDF\SuperShopDF.Web\Views\Orders\Index.cshtml(42)
              ---------------------------------------------*/

            // 1.01.09 vídeo ASP-NET_MVC_22
            // 1.01.09 vídeo ASP-NET_MVC_22
            return View(model); // 1.01.09 vídeo ASP-NET_MVC_22

        } // end Create()


        // 27.17 - vídeo ASP-NET_MVC_23:
        public IActionResult AddProduct()
        {
            var model = new AddItemViewModel
            {
                Quantity = 1,
                Products = _productRepository.GetComboProducts()
            };

            return View(model);
        } // end AddProduct()


        // 16.40 - vídeo ASP-NET_MVC_24:
        [HttpPost]
        public async Task<IActionResult> AddProduct(AddItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _orderRepository.AddItemToOrderAsync(model, this.User.Identity.Name);
                return RedirectToAction("Create");
            }
            return View(model);
        } // end AddProduct()


    } // class class OrdersController 
} // end using Microsoft.AspNetCore.Mvc;
