using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels.Attributes;
using Fanex.Affiliate.User.Core.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.User.Core.MetalModels
{
    public class UserSignUpParam
    {
        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "FirstNameIsRequired")]
        [MaxLength(Number.Fifty, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "FirstNameMaxLength")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "LastNameIsRequired")]
        [MaxLength(Number.Fifty, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "LastNameMaxLength")]
        public string LastName { get; set; }

        public bool Gender { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "EmailAddressIsRequired")]
        [EmailAddress(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "EmailAddressIsInvalid")]
        [MaxLength(Number.Fifty, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "EmailMaxLength")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "errContactNumberEmpty")]
        [MaxLength(Number.Fifty, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "PhoneMaxLength")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "AddressIsRequired")]
        [MaxLength(Number.TwoFiveZero, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "AddressMaxLength")]
        public string Address { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "CityIsRequired")]
        [MaxLength(Number.TwoFiveZero, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "CityMaxLength")]
        public string City { get; set; }

        public string State { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "PostcodeZiIsRequired")]
        [MaxLength(Number.Fifty, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "PostalMaxLength")]
        public string Postal { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "CountryErr")]
        public string Country { get; set; }

        [RequiredByChecked(DependentPropertyName = "HasWebSite", IsChecked = true, ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "WebsiteURLIsRequired")]
        [MaxLength(Number.OneHundred, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "SiteUrlMaxLength")]
        public string SiteUrl { get; set; } = string.Empty;

        public string MoreSiteUrls { get; set; }

        [RequiredByChecked(DependentPropertyName = "HasWebSite", IsChecked = false, ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "DescWebsite")]
        [MaxLength(Number.OneThousand, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "NoSiteDescMaxLength")]
        public string NoSiteDesc { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "TargetCountryErr")]
        public string TargetCountryCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "AffAccountLanguageErr")]
        public string PreferredLanguage { get; set; }

        public static string PreferredPayment => "Player";

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "BETUserNameIsRequired")]
        [MaxLength(Number.Thirty, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "UserNameMaxLength30")]
        public string Player_UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "errSecQuestionEmpty")]
        public byte QuestionId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "errYourAnswer")]
        [MaxLength(Number.ThreeHundred, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "AnswerMaxLength")]
        public string Answer { get; set; }

        public string ContactApps { get; set; }

        public string Ip { get; set; }

        public string Language { get; set; }

        public string UserName { get; set; }

        public string AffType { get; set; }

        public string AffParentCode { get; set; }

        public int Player_CustId { get; set; }

        public int Player_Currency { get; set; }

        public IEnumerable<ContactApp> ContactAppList { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "VerifyEmailAddressIsRequired")]
        [Compare("Email", ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "Emailaddressest")]
        public string VerifyEmail { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "ErrmsgEntryValidCode")]
        public string Captcha { get; set; }

        public bool IsLocal { get; set; }

        public bool HasWebSite { get; set; } = true;

        public bool IsValidParentCode { get; set; } = true;
    }
}
