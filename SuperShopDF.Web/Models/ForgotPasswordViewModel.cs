using System.ComponentModel.DataAnnotations;

namespace SuperShopDF.Web.Models
{
    public class ForgotPasswordViewModel
    {
        // ----------------------------------------------------
        // PlayList: ASP.NET core tutorial for beginners
        // PL_27_(115-124)_Forgot password in asp net core.mp4
        // ----------------------------------------------------

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    } // end class ForgotPasswordViewModel
} // end namespace SuperShopDF.Web.Models
