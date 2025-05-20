using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fanex.Affiliate.Admin.Core.Attributes;

namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class UserAccountParam : AffiliateBasicInfoParam
    {
        [ConditionalRequired("IsMarketingAccount")]
        public string FirstName { get; set; }

        [ConditionalRequired("IsMarketingAccount")]
        public string LastName { get; set; }

        [ConditionalRequired("IsMarketingAccount")]
        public bool Gender { get; set; }

        [ConditionalRequired("IsMarketingAccount")]
        [EmailAddress]
        public string Email { get; set; }

        [ConditionalRequired("IsMarketingAccount")]
        public string Phone { get; set; }

        public string ContactApps { get; set; }

        [ConditionalRequired("IsMarketingAccount")]
        public string Address { get; set; }

        [ConditionalRequired("IsMarketingAccount")]
        public string City { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string State { get; set; }

        [ConditionalRequired("IsMarketingAccount")]
        public string Postcode { get; set; }

        [ConditionalRequired("IsMarketingAccount")]
        public string Country { get; set; }


        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string SiteUrl { get; set; }


        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string MoreSiteUrls { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string NoSiteDescription { get; set; }

        [ConditionalRequired("IsMarketingAccount")]
        public string PreferredPayment { get; set; }

        [SpParam(Ignored = true)]
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

        public bool IsMarketingAccount => false;
    }
}
