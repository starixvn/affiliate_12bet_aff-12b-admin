using System.ComponentModel.DataAnnotations;
using Fanex.Data;

namespace Fanex.Affiliate.User.Core.MetalModels
{
    public class UserSignUpParam
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public bool Gender { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string MobilePhone { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        public string State { get; set; }

        [Required]
        public string Postal { get; set; }

        [Required]
        public string Country { get; set; }

        public bool EmailFormat { get; set; }

        public string SiteUrl { get; set; }

        public string MoreSiteUrls { get; set; }

        public string NoSiteDesc { get; set; }

        [Required]
        public string PreferredLanguage { get; set; }

        public string PreferredPayment => "Player";

        [Required]
        public string Player_UserName { get; set; }

        [Required]
        public string Question { get; set; }

        [Required]
        public string Answer { get; set; }

        public string Ip { get; set; }

        public string Browser { get; set; }

        public string Language { get; set; }

        public string UserName { get; set; }

        public string AffMaster { get; set; }

        public byte AffType { get; set; }

        public int AffId { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        [SpParam(Ignored = true)]
        public string Captcha { get; set; }

        [SpParam(Ignored = true)]
        public bool IsLocal { get; set; }
    }
}
