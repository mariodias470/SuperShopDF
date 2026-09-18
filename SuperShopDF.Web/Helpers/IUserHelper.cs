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
            5) UpdateUserAsync(User user);                            // 09.35 do vídeo ASP.NET_MVC_18
            6) ChangePasswordAsync(User user, string oldPassword, 
                                   string newPassword);               // 09.35 do vídeo ASP.NET_MVC_18
            7) CheckRoleAsync(User user, string roleName);            // 05.43 - ASP.NET_MVC_19 
            8) IsUserInRoleAsync(User user, string roleName);         // 09.05 - ASP.NET_MVC_19 
         */


        Task<User> GetUserByEmailAsync(string email);
        
        Task<IdentityResult> AddUserAsync(User user, string password);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        // 09.35 do vídeo ASP.NET_MVC_18:
        Task<IdentityResult> UpdateUserAsync(User user);

        // 09.35 do vídeo ASP.NET_MVC_18:
        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);

        // 05.43 - ASP.NET_MVC_19 
        // Verifica se ele tem o roleName especificado. Em caso não afirmativo cria-o.
        Task CheckRoleAsync(string roleName);

        // 09.05 - ASP.NET_MVC_19 
        // Relaciona o roleName especificado ao utilizador .
        Task AddUserToRoleAsync(User user, string roleName);

        // Verifica se o utilizador já tem o 'roleName' especificado. Atenção que um utilizador pode ter mais do que um role.
        Task<bool> IsUserInRoleAsync(User user, string roleName);
    } // end interface IUserHelper
} // end namespace SuperShopDF.Web.Helpers
