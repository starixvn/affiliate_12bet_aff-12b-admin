using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels.Attributes;
using Fanex.Affiliate.User.Core.Resources;
using Fanex.Data;

namespace Fanex.Affiliate.User.Core.MetalModels
{
    public class UserSignUpParam
    {
        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "FirstNameIsRequired")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "LastNameIsRequired")]
        public string LastName { get; set; }

        public bool Gender { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "EmailAddressIsRequired")]
        [EmailAddress(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "EmailAddressIsInvalid")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "errContactNumberEmpty")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "AddressIsRequired")]
        public string Address { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "CityIsRequired")]
        public string City { get; set; }

        public string State { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "PostcodeZiIsRequired")]
        public string Postal { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "CountryErr")]
        public string Country { get; set; }

        [RequiredByChecked(DependentPropertyName = "HasWebSite", IsChecked = true, ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "WebsiteURLIsRequired")]
        [SuppressMessage("Sonar Code Smell", "S3996:UriPropertiesShouldNotBeStrings", Justification = "Reviewed. Suppression is OK here.")]
        public string SiteUrl { get; set; } = string.Empty;

        public string MoreSiteUrls { get; set; }

        [RequiredByChecked(DependentPropertyName = "HasWebSite", IsChecked = false, ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "DescWebsite")]
        public string NoSiteDesc { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "PreferLangErr")]
        public string PreferredLanguage { get; set; }

        public static string PreferredPayment => "Player";

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "BETUserNameIsRequired")]
        public string Player_UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "errSecQuestionEmpty")]
        public byte QuestionId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "errYourAnswer")]
        public string Answer { get; set; }

        public string ContactApps { get; set; }

        public string Ip { get; set; }

        public string Language { get; set; }

        public string UserName { get; set; }

        public string AffType { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        [SpParam(Ignored = true)]
        public IEnumerable<ContactApp> ContactAppList { get; set; }

        [SpParam(Ignored = true)]
        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "EmailAddressIsRequired")]
        [Compare("Email", ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "Emailaddressest")]
        public string VerifyEmail { get; set; }

        [SpParam(Ignored = true)]
        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "ErrmsgEntryValidCode")]
        public string Captcha { get; set; }

        [SpParam(Ignored = true)]
        public bool IsLocal { get; set; }

        [SpParam(Ignored = true)]
        public bool HasWebSite { get; set; } = true;
    }
}
