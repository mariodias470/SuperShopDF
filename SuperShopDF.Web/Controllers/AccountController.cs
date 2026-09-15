using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SuperShopDF.Web.Data.Entities;
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

        /*
            private readonly IUserHelper _userHelper; // 10.28 do vídeo ASP.NET_MVC_16
            AccountController(IUserHelper userHelper) 

            public async Task<IActionResult>    Login(LoginViewModel model)
            public async Task<IActionResult>    Logout()
            public IActionResult                Register() 

         */


        private readonly IUserHelper _userHelper; // 10.28 do vídeo ASP.NET_MVC_16

        public AccountController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
        }

        // 29.43 do vídeo ASP.NET_MVC_16:
        // Este método limita-se a mostrar a primeira página do login.
        /*---------------------------------------------
         | Login()
         +---------------------------------------------*/
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        } // end Login()



        /*---------------------------------------------
         | Login()
         +---------------------------------------------*/
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


        /*---------------------------------------------
         | Logout()
         +---------------------------------------------*/
        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();

            return RedirectToAction("Index", "Home");
        } // end Logout()


        /*---------------------------------------------
         | Register()
         +---------------------------------------------*/
        // 8.20 do vídeo ASP.NET_MVC_17:
        [HttpGet]
        public IActionResult Register() // vide Views/Account/Register.cshtml:
        {                               // <a asp-action="Register" class="btn btn-primary">Register New User</a>
            return View();
        } // end Register()





        /*---------------------------------------------
         | Register()
         +---------------------------------------------*/
        // 12.41 do vídeo ASP.NET_MVC_17:
        [HttpPost]
        public async Task<IActionResult> Register(RegisterNewUserViewModel model) // vide Views/Account/Register.cshtml:
        {                                     // <a asp-action="Register" class="btn btn-primary">Register New User</a>
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username);

                if (user == null)
                {
                    user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Username,
                        UserName = model.Username
                    };
                    // Adicionar o novo user:
                    var result = await _userHelper.AddUserAsync(user, model.Password);
                    if (result != IdentityResult.Success)
                    {
                        // 16.45 vídeo ASP.NET_MVC_17, podia ter usado um viewbag ou outra coisa qualquer...
                        // Não conseguiu adicionar o novo user.
                        ModelState.AddModelError(string.Empty, "The user couldn't be created!...");
                        return View(model);
                    }
                    // user criado.
                    var loginViewModel = new LoginViewModel
                    {
                        Password = model.Password,
                        RememberMe = false,
                        Username = model.Username,
                    };
                    // Tentativa de login:
                    var result2 = await _userHelper.LoginAsync(loginViewModel);
                    
                    if (result2.Succeeded)
                    {
                        // 21.10 vídeo ASP.NET_MVC_17: Se Consegui fazer login:
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError(string.Empty, "The user couldn't be logged!...");
                }
            }
            return View(model);
        } // end Register()


        // teste de um vídeo (COMENTAR POSTERIORMENTE)
        // vide tb ficheiros:  ForgotPassword.cshtml
        //                     ForgotPasswordViewModel.cs
        /*------------------------------------------------
         | ForgotPassword()
         +------------------------------------------------*/
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword() 
        {
            return View();
        } // end ForgotPassword()




    } // end class AccountController 
} // end namespace SuperShopDF.Web.Controllers
