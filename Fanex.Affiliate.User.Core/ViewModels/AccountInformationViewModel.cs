using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.ViewModels
{
    public class AccountInformationViewModel
    {
        public int AffId { get; set; }

        public string UserName { get; set; }

        public bool Gender { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Postal { get; set; }

        public string Country { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string MobilePhone { get; set; }

        public string SiteUrl { get; set; }

        public string MoreSiteUrls { get; set; }

        public string NoSiteDesc { get; set; }

        public string PreferredLanguage { get; set; }

        public string PreferredPayment { get; set; }

        public string PlayerUsername { get; set; }

        public string State { get; set; }

        public bool IsRenderState { get; set; }

        public bool IsPlayerAccount => !string.IsNullOrEmpty(PreferredPayment) && PreferredPayment.Equals("Player", StringComparison.OrdinalIgnoreCase);

        public IEnumerable<string> MoreWebsiteUrlList => string.IsNullOrEmpty(MoreSiteUrls) ? new string[0] : MoreSiteUrls.Split(';');

        public string LanguageName { get; set; }
    }
}