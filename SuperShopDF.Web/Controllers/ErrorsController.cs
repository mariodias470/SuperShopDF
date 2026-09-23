using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using SuperShopDF.Web.Models;


namespace SuperShopDF.Web.Controllers
{
    // 18.50 do vídeo ASP.NET_MVC_27:

    public class ErrorsController : Controller
    {
        // 19.22 do vídeo ASP.NET_MVC_27:
        // código copiado do ficheiro HomeController.cs:

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // 25.11 ASP.NET_MVC_21:
        [Route("error/404")]
        public IActionResult Error404()
        {
            return View();
        } // end Error404()

    } // class end ErrorsController 
} // end namespace SuperShopDF.Web.Controllers
