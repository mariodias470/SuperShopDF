using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SuperShopDF.Web.Helpers;
using SuperShopDF.Web.Models;

namespace SuperShopDF.Web.Controllers
{
    // 12.21 do vídeo ASP.NET_MVC_16
    //      Estamos cada vez vez mais próximos do cliente.
    //      Começámos no back-end e estamos a caminhar em direcção ao Front-End.
    //      A seguir à action vem a view
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper; // 10.28 do vídeo ASP.NET_MVC_16

        public AccountController(IUserHelper userHelper) 
        {
            _userHelper = userHelper;
        }

        // 29.43 do vídeo ASP.NET_MVC_16:
        // Este método limita-se a mostrar a primeira página do login.
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        } // end Login()


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // 30.18 do vídeo ASP.NET_MVC_16:
            // Se o modelo fôr válido, i.e., se estiver tudo preenchido de acôrdo com as anottations...
            if (ModelState.IsValid)
            {
                var result = await _userHelper.LoginAsync(model);
                if (result.Succeeded) 
                {
                    if (this.Request.Query.Keys.Contains("ReturnUrl")) 
                    {
                        // 40.25 do vídeo ASP.NET_MVC_16
                        //      Url de retorno
                        // https://localhost:44334/Account/Login?ReturnUrl=%2FProducts	 
                        return Redirect(this.Request.Query["ReturnUrl"].First());
                    }
                    return this.RedirectToAction("Index", "Home");
                }
            }

            this.ModelState.AddModelError(string.Empty, "Failed to Login");

            return View(model);
        } // end Login()



        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();

            return RedirectToAction("Index", "Home");
        }

    } // end class AccountController 
} // end namespace SuperShopDF.Web.Controllers
