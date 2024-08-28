using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class UserAccountParam : AffiliateBasicInfoParam
    {
        [SpParam(Ignored = true)]
        public bool IsPendingAff { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public bool Gender { get; set; }

        [EmailAddress]
        [SpParamProtecting(Function.AffEmail)]
        public string Email { get; set; }

        [SpParamProtecting(Function.AffPhone)]
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

        [SpParamProtecting(Function.AffWebUrl)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string SiteUrl { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [SpParamProtecting(Function.AffWebUrl)]
        public string MoreSiteUrls { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [SpParamProtecting(Function.AffWebUrl)]
        public string NoSiteDescription { get; set; }

        [SpParamProtecting(Function.AffPaymentInfo)]
        public string PreferredPayment { get; set; }

        [SpParamProtecting(Function.AffPaymentInfo)]
        public string PlayerUsername { get; set; }

        [SpParamProtecting(Function.AffPaymentInfo)]
        public string NetellerAccountId { get; set; }

        [SpParamProtecting(Function.AffPaymentInfo)]
        public string MoneybookersEmail { get; set; }

        [SpParamProtecting(Function.AffPaymentInfo)]
        public string BankName { get; set; }

        [SpParamProtecting(Function.AffPaymentInfo)]
        public string BankAddress { get; set; }

        [SpParamProtecting(Function.AffPaymentInfo)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string BankCode { get; set; }

        [SpParamProtecting(Function.AffPaymentInfo)]
        public string BankAccountName { get; set; }

        [SpParamProtecting(Function.AffPaymentInfo)]
        public string BankAccountNumber { get; set; }

        public bool MailOption { get; set; }

        [SpParamProtecting(Function.SecurityQA)]
        public int? QuestionId { get; set; }

        [SpParamProtecting(Function.SecurityQA)]
        public string Answer { get; set; }

        public IEnumerable<ContactApp> ContactAppList { get; set; }

        public bool HasWebSite { get; set; }
    }
}
