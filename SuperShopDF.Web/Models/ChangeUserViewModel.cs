using System.ComponentModel.DataAnnotations;

namespace SuperShopDF.Web.Models
{
    // Ficheiro criado no início do vídeo ASP-NET_MVC_18.
    public class ChangeUserViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

    } //end class ChangeUserViewModel
} // end namespace SuperShopDF.Web.Models
