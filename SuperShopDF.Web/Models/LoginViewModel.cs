using System.ComponentModel.DataAnnotations;

namespace SuperShopDF.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }
        
        [Required]
        [MinLength(6)]
        public string Password { get; set; }    

        public bool RememberMe { get; set; }
    } // end class LoginViewModel
} // end namespace SuperShopDF.Web.Models
