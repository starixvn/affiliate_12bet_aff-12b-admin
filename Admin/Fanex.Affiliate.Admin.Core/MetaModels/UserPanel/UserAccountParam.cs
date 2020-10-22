using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class UserAccountParam : AffiliateBasicInfoParam
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public bool Gender { get; set; }

        [Required]
        [EmailAddress]
        [SpParamProtecting(Function.AffEmail, Function.WritePermission)]
        public string Email { get; set; }

        [Required]
        [SpParamProtecting(Function.AffPhone, Function.WritePermission)]
        public string Phone { get; set; }

        public string ContactApps { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string State { get; set; }

        [Required]
        public string Postcode { get; set; }

        [Required]
        public string Country { get; set; }

        [SuppressMessage("Sonar Code Smell", "S3996:UriPropertiesShouldNotBeStrings", Justification = "Reviewed. Suppression is OK here.")]
        [Required]
        [SpParamProtecting(Function.AffWebUrl, Function.WritePermission)]
        public string SiteUrl { get; set; }

        [SuppressMessage("Sonar Code Smell", "S3996:UriPropertiesShouldNotBeStrings", Justification = "Reviewed. Suppression is OK here.")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [SpParamProtecting(Function.AffWebUrl, Function.WritePermission)]
        public string MoreSiteUrls { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [SpParamProtecting(Function.AffWebUrl, Function.WritePermission)]
        public string NoSiteDescription { get; set; }

        [Required]
        [SpParamProtecting(Function.AffPaymentInfo, Function.WritePermission)]
        public string PreferredPayment { get; set; }

        [SpParamProtecting(Function.AffPaymentInfo, Function.WritePermission)]
        public string PlayerUsername { get; set; }

        [SpParamProtecting(Function.AffPaymentInfo, Function.WritePermission)]
        public string NetellerAccountId { get; set; }

        [SpParamProtecting(Function.AffPaymentInfo, Function.WritePermission)]
        public string MoneybookersEmail { get; set; }

        [SpParamProtecting(Function.AffPaymentInfo, Function.WritePermission)]
        public string BankName { get; set; }

        [SpParamProtecting(Function.AffPaymentInfo, Function.WritePermission)]
        public string BankAddress { get; set; }

        [SpParamProtecting(Function.AffPaymentInfo, Function.WritePermission)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string BankCode { get; set; }

        [SpParamProtecting(Function.AffPaymentInfo, Function.WritePermission)]
        public string BankAccountName { get; set; }

        [SpParamProtecting(Function.AffPaymentInfo, Function.WritePermission)]
        public string BankAccountNumber { get; set; }

        public bool MailOption { get; set; }

        [SpParamProtecting(Function.SecurityQa, Function.WritePermission)]
        public int? QuestionId { get; set; }

        [SpParamProtecting(Function.SecurityQa, Function.WritePermission)]
        public string Answer { get; set; }

        public IEnumerable<ContactApp> ContactAppList { get; set; }
    }
}
