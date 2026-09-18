using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
            private readonly IUserHelper              _userHelper; // 10.28 do vídeo ASP.NET_MVC_16
            1)                                        AccountController(IUserHelper userHelper) 

            2) public IActionResult                   Login()
            3) public async Task<IActionResult>       Login(LoginViewModel model) || [HttpPost]

            4) public async Task<IActionResult>       Logout()
    
            5) public IActionResult                   Register() || [HttpGet] // vide Views/Account/Register.cshtml
            6) public async Task<IActionResult>       Register(RegisterNewUserViewModel model) || [HttpPost] // vide Views/Account/Register.cshtml
            
            7) public async Task<IActionResult>       ChangeUser()
            8) public async Task<IActionResult>       ChangeUser()
            9) public IActionResult                   ChangePassword()

                // teste de um vídeo (COMENTAR POSTERIORMENTE)
                // vide tb ficheiros:  ForgotPassword.cshtml
                //                     ForgotPasswordViewModel.cs
                10) IActionResult                      ForgotPassword() || [HttpGet]
         */

        private readonly IUserHelper _userHelper; // 10.28 do vídeo ASP.NET_MVC_16

        // 1)
        public AccountController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
        }

        // 29.43 do vídeo ASP.NET_MVC_16:
        // Este método limita-se a mostrar a primeira página do login.
        /*---------------------------------------------
         | 2) Login()
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
         | 3) Login()
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
         | 4) Logout()
         +---------------------------------------------*/
        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();

            return RedirectToAction("Index", "Home");
        } // end Logout()


        /*---------------------------------------------
         | 5) Register()
         +---------------------------------------------*/
        // 8.20 do vídeo ASP.NET_MVC_17:
        [HttpGet]
        public IActionResult Register() // vide Views/Account/Register.cshtml:
        {                               // <a asp-action="Register" class="btn btn-primary">Register New User</a>
            return View();
        } // end Register()



        /*---------------------------------------------
         | 6) Register()
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
                    // TODO: role customer (TPC). Vídeo 19 aos 28m 08s. Na prática faz-se como está no seed. Vide também 21m 44s do mesmo vídeo.

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


        // 13.10 vídeo ASP.NET_MVC_18:
        /*---------------------------------------------
         | 7) ChangeUser()
         +---------------------------------------------*/
        public async Task<IActionResult> ChangeUser()
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            var model = new ChangeUserViewModel();
            if (user != null) 
            {
 //             Begin delta1:
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
//              End delta1:
            }
            return View(model);
            // 15.55 vídeo ASP.NET_MVC_18: Aqui estamos a fazer o inverso.
            //                             Quando esta action chamar a view passa o modelo preenchido lá para dentro.
            //                             Para depois podermos alterá-lo.
            // 16.17 vídeo ASP.NET_MVC_18: Agora vamos criar a view (é sempre a mesma coisa).
        } // end ChangeUser()



        // 19.37 vídeo ASP.NET_MVC_18:
        /*---------------------------------------------
         | 8) ChangeUser()
         +---------------------------------------------*/
        [HttpPost]
        public async Task<IActionResult> ChangeUser(ChangeUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user != null)
                { // O inverso de delta1:
                    user.FirstName = model.FirstName;
                    user.LastName =  model.LastName;
                    var response = await _userHelper.UpdateUserAsync(user);
                    if (response.Succeeded)
                    {
                        ViewBag.UserMessage = "User updated!";
                    }
                    else 
                    {
                        ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);
                    }
                }
            }
            return View(model);
        } // end ChangeUser()



        // 26.15 vídeo ASP.NET_MVC_18:
        /*---------------------------------------------
         | 9) ChangePassword()
         +---------------------------------------------*/
        public IActionResult ChangePassword()
        {
            return View();
        } // end ChangePassword()



        // 30.15 vídeo ASP.NET_MVC_18:
        /*---------------------------------------------
         | 10) ChangePassword()
         +---------------------------------------------*/
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user != null)
                {
                    var result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return this.RedirectToAction("ChangeUser");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                    }
                }
            }
            else 
            {
                this.ModelState.AddModelError(string.Empty, "User not found!...");
            }
            return View(model);
        } // end ChangePassword()



        // teste de um vídeo (COMENTAR POSTERIORMENTE)
        // vide tb ficheiros:  ForgotPassword.cshtml
        //                     ForgotPasswordViewModel.cs
        /*------------------------------------------------
         | 10) ForgotPassword()
         +------------------------------------------------*/
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword() 
        {
            return View();
        } // end ForgotPassword()



    } // end class AccountController 
} // end namespace SuperShopDF.Web.Controllers
