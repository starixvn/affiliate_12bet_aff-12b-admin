using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fanex.Affiliate.Admin.Core.Attributes;

namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class UserAccountParam : AffiliateBasicInfoParam
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RequiredDependOnField(nameof(IsNormalAccount))]
        public string FirstName { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RequiredDependOnField(nameof(IsNormalAccount))]
        public string LastName { get; set; }

        [RequiredDependOnField(nameof(IsNormalAccount))]
        public bool Gender { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RequiredDependOnField(nameof(IsNormalAccount))]

        public string Phone { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RequiredDependOnField(nameof(IsNormalAccount))]
        public string ContactApps { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RequiredDependOnField(nameof(IsNormalAccount))]
        public string Address { get; set; }
        
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RequiredDependOnField(nameof(IsNormalAccount))]
        public string City { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RequiredDependOnField(nameof(IsNormalAccount))]
        public string State { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RequiredDependOnField(nameof(IsNormalAccount))]
        public string Postcode { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RequiredDependOnField(nameof(IsNormalAccount))]
        public string Country { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string SiteUrl { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string MoreSiteUrls { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string NoSiteDescription { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RequiredDependOnField(nameof(IsNormalAccount))]
        public string PreferredPayment { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RequiredDependOnField(nameof(IsNormalAccount))]
        public string PlayerUsername { get; set; }

        public string NetellerAccountId { get; set; }

        public string MoneybookersEmail { get; set; }

        public string BankName { get; set; }

        public string BankAddress { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string BankCode { get; set; }

        public string BankAccountName { get; set; }

        public string BankAccountNumber { get; set; }

        public bool MailOption { get; set; }

        public IEnumerable<ContactApp> ContactAppList { get; set; }

        public bool HasWebSite { get; set; }

        public int? Player_CustId { get; set; }

        public int? Player_Currency { get; set; }

        [SpParam(Ignored = true)]
        public bool HasHostAffPersonalInfoPermission { get; set; }

        public bool IsMarketingAccount { get; set; }

        public bool IsNormalAccount => !IsMarketingAccount;

    }
}
