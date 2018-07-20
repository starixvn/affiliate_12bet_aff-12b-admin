using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.DTO.Affiliate
{
    public class AffInfo
    {
        public int AffId { get; set; }

        public string Status { get; set; }

        public bool Closed { get; set; }

        public bool IsLocked { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public string LastLoginIP { get; set; }

        public string UserName { get; set; }

        public string UserName2 { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool? Gender { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string ContactApps { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Postal { get; set; }

        public string Country { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public bool MailOption { get; set; }

        public string SiteUrl { get; set; }

        public string MoreSiteUrls { get; set; }

        public string NoSiteDesc { get; set; }

        public string PreferredPayment { get; set; }

        public string PreferredLanguage { get; set; }

        public string PlayerUsername { get; set; }

        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public decimal BalBroughtForward { get; set; }

        public string NetellerAccountId { get; set; }

        public string MoneybookersEmail { get; set; }

        public string WireBankName { get; set; }

        public string WireBankAddress { get; set; }

        public string WireSwiftCode { get; set; }

        public string WireAccountName { get; set; }

        public string WireAccountNo { get; set; }

        public byte AffType { get; set; }

        public IEnumerable<ContactApp> ContactAppList { get; set; }

        public string InternalUrl { get; set; }
    }
}
