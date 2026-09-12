using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SuperShopDF.Web.Data.Entities;
using SuperShopDF.Web.Models;


namespace SuperShopDF.Web.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();
    } // end interface IUserHelper

} // end namespace SuperShopDF.Web.Helpers
