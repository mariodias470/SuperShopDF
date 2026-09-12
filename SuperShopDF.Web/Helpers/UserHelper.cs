using SuperShopDF.Web.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SuperShopDF.Web.Data.Entities;


namespace SuperShopDF.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        // public UserHelper(UserManager<User> userManager) // 46.58 
        public UserHelper(UserManager<User> userManager, SignInManager<User> signInManager) // 05.26 ASP.NET_MVC_16
        {
            this._userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync
                (
                    model.Username,
                    model.Password,
                    model.RememberMe,
                    false
                );
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }


        // 48.35 ASP.NET_MVC_10 - Não utilizaremos directamente o UserManager, mas sim o UserHelper.

    } // end class UserHelper
} // end namespace SuperShopDF.Web.Helpers
