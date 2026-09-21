using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SuperShopDF.Web.Data.Entities
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Display(Name = "Full Name")] // Aos 1.07.38 do vídeo ASP.NET_MVC_22:
        public string FullName => $"{FirstName} {LastName}";

    } // end class User
} // end namespace SuperShopDF.Web.Data.Entities
