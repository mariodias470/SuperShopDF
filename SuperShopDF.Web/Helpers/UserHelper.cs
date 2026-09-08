using Microsoft.AspNetCore.Identity;
using SuperShopDF.Web.Data.Entities;
using System.Threading.Tasks;

namespace SuperShopDF.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> _userManager;

        public UserHelper(UserManager<User> userManager) // 46.58 
        {
            this._userManager = userManager;
        }

        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        // 48.35 ASP.NET_MVC_10 - Não utilizaremos directamente o UserManager, mas sim o UserHelper.

    } // end class UserHelper
} // end namespace SuperShopDF.Web.Helpers
