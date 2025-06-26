using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;

namespace Fanex.Affiliate.Admin.Core.DTO.Affiliate
{
    public class MarketingAccount
    {
        // General Info
        public int AffId { get; set; }

        public string ParentAffName { get; set; }

        public string AffCode { get; set; }

        public string UserName { get; set; }

        public string Status { get; set; }

        public bool IsPrivate { get; set; }

        public int AffType { get; set; }

        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public string PreferredLanguage { get; set; }

        public string TargetCountryName { get; set; }

        public string TargetCountryCode { get; set; }

        // Personal
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Gender { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string ContactApps { get; set; }

        // Additional Info

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Postal { get; set; }

        public string Country { get; set; }

        public string Question { get; set; }

        public int QuestionId { get; set; }

        public string Answer { get; set; }

        // Site 
        public string SiteUrl { get; set; }

        public string MoreSiteUrls { get; set; }

        public string NoSiteDesc { get; set; }

        // Payment 
        public string PreferredPayment { get; set; }

        public string PlayerUsername { get; set; }

        public int PlayerCustId { get; set; }

        public string PlayerCurrency { get; set; }

        public string PlayerCurrencyId { get; set; }

        public bool? MailOption { get; set; }

        public bool HasHostAffPersonalInfoPermission { get; set; }

        public IEnumerable<ContactApp> ContactAppList { get; set; }

        public bool HasWebSite => !string.IsNullOrEmpty(SiteUrl);
    }
}
