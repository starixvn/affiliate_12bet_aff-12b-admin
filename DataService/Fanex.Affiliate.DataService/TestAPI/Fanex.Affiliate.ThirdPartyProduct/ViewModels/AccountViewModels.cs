using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.ThirdPartyProduct.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User")]
        public string User { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

}
