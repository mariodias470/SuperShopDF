using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SuperShopDF.Web.Data.Entities;
using SuperShopDF.Web.Models;

namespace SuperShopDF.Web.Helpers
{
    public interface IUserHelper
    {
        /*
            1) GetUserByEmailAsync(string email);
            2) AddUserAsync(User user, string password);
            3) LoginAsync(LoginViewModel model);
            4) LogoutAsync();
            5) UpdateUserAsync(User user);
            6) ChangePasswordAsync(User user, string oldPassword, string newPassword); 
         */

        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        // 09.35 do vídeo ASP.NET_MVC_18:
        Task<IdentityResult> UpdateUserAsync(User user);
        // 09.35 do vídeo ASP.NET_MVC_18:
        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword); 


    } // end interface IUserHelper

} // end namespace SuperShopDF.Web.Helpers
