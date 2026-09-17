using System.ComponentModel.DataAnnotations;

namespace SuperShopDF.Web.Models
{
    // 3.52 --> Ficheiro criado no início do vídeo ASP-NET_MVC_18.
    public class ChangePasswordViewModel
    {
        [Required]
        [Display(Name ="Current password")]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }


        [Required]
        [Compare("NewPassword")]
        public string Confirm { get; set; }

    } //end class ChangePasswordViewModel
} // end namespace SuperShopDF.Web.Models
