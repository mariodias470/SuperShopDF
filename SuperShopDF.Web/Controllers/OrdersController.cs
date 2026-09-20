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
            return View();
        }

    } // class class OrdersController 
} // end using Microsoft.AspNetCore.Mvc;
