using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperShopDF.Web.Data;
using System.Threading.Tasks;

namespace SuperShopDF.Web.Controllers
{
    // 58.43 - vídeo ASP-NET_MVC_22
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
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

    } // class class OrdersController 
} // end using Microsoft.AspNetCore.Mvc;
