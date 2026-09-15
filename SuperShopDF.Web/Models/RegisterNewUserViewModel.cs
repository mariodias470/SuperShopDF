using System.ComponentModel.DataAnnotations;

namespace SuperShopDF.Web.Models
{
    // Ficheiro criado aos 1.00 do vídeo ASP.NET_MVC_17

    public class RegisterNewUserViewModel
    {
        [Required]
        [Display(Name="First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)] // <=> [EmailAddress] 
        public string Username { get; set; }

        [Required]
        [MinLength(6)] // <=> [EmailAddress] 
        public string Password { get; set; }

        [Required]
        [Compare("Password")] // <=> [EmailAddress] 
        public string Confirm { get; set; }

    } // end class RegisterNewUserViewModel
} // end namespace SuperShopDF.Web.Models
