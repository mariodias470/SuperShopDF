using SuperShopDF.Web.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SuperShopDF.Web.Data.Entities;

namespace SuperShopDF.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        /*
            1) GetUserByEmailAsync(string email);
            2) AddUserAsync(User user, string password);
            3) LoginAsync(LoginViewModel model);
            4) LogoutAsync();
            5) UpdateUserAsync(User user);
            6) ChangePasswordAsync(User user, string oldPassword, string newPassword); 
         */

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;



        // public UserHelper(UserManager<User> userManager) // 46.58 
        public UserHelper(UserManager<User> userManager, SignInManager<User> signInManager) // 05.26 ASP.NET_MVC_16
        {
            this._userManager = userManager;
            _signInManager = signInManager;
        }


        // 1) 
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        } // end GetUserByEmailAsync()


        // 2) 
        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        } // end AddUserAsync()


        // 3) 
        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync
                (
                    model.Username,
                    model.Password,
                    model.RememberMe,
                    false
                );
        } // end LoginAsync()


        // 4)
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        } // end LogoutAsync()


        // 11.25 do vídeo ASP.NET_MVC_18:
        // 5)
        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await _userManager.UpdateAsync(user);
        } // end UpdateUserAsync()


        // 11.25 do vídeo ASP.NET_MVC_18:
        // 6
        public async Task<IdentityResult> ChangePasswordAsync
            (
                User user, 
                string oldPassword, 
                string newPassword
            )
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        } // end ChangePasswordAsync()


        // 48.35 ASP.NET_MVC_10 - Não utilizaremos directamente o UserManager, mas sim o UserHelper.



    } // end class UserHelper
} // end namespace SuperShopDF.Web.Helpers
