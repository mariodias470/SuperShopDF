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
            7) Task CheckRoleAsync(string v);
            8) CheckRoleAsync(User user, string roleName);      
            9) IsUserInRoleAsync(User user, string roleName);   
         */

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        // public UserHelper(UserManager<User> userManager) // 46.58 
        // public UserHelper(UserManager<User> userManager, SignInManager<User> signInManager) // 05.26 - ASP.NET_MVC_16
        public UserHelper  // 01.20 - ASP.NET_MVC_19
            (
                UserManager<User> userManager, 
                SignInManager<User> signInManager,
                RoleManager<IdentityRole> roleManager // 01.20 - ASP.NET_MVC_19
            )
        {
            this._userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        } // end UserHelper()


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


        // 5)
        // 11.25 do vídeo ASP.NET_MVC_18:
        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await _userManager.UpdateAsync(user);
        } // end UpdateUserAsync()

        
        // 6
        // 11.25 do vídeo ASP.NET_MVC_18:
        public async Task<IdentityResult> ChangePasswordAsync
            (
                User user, 
                string oldPassword, 
                string newPassword
            )
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        } // end ChangePasswordAsync()

        // 7)
        // 05.43 - ASP.NET_MVC_19 
        public async Task CheckRoleAsync(string roleName) 
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists) 
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = roleName });
            } 

        } // end CheckRoleAsync()

        // 12.35 - ASP.NET_MVC_19: 
        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        } // end AddUserToRoleAsync()

        // 13.26 - ASP.NET_MVC_19:
        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        } // end IsUserInRoleAsync()


        //7) CheckRoleAsync(User user, string roleName);            // 05.43 - ASP.NET_MVC_19 
        //8) IsUserInRoleAsync(User user, string roleName);         // 09.05 - ASP.NET_MVC_19 


        // 48.35 ASP.NET_MVC_10 - Não utilizaremos directamente o UserManager, mas sim o UserHelper.


    } // end class UserHelper
} // end namespace SuperShopDF.Web.Helpers
