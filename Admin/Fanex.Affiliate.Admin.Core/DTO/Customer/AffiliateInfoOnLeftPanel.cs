using System;
using Newtonsoft.Json;

namespace Fanex.Affiliate.Admin.Core.DTO.Customer
{
    public class AffiliateInfoOnLeftPanel
    {
        public int AffId { get; set; }

        public int GroupId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Gender { get; set; }

        public string ContactApps { get; set; }

        public string SiteUrl { get; set; }

        public string MoreSiteUrl { get; set; }

        public string NoSiteDescription { get; set; }

        [JsonIgnore]
        public DateTime? LastLoginDate { get; set; }

        public string LastLoginDateText => LastLoginDate.HasValue ? LastLoginDate.Value.ToString("MM/dd/yyyy HH:mm:ss") : string.Empty;

        public string LastLoginIp { get; set; }

        public string CommGroupName { get; set; }

        public decimal BalanceBf { get; set; }

        public string Status { get; set; }

        public bool IsClosed { get; set; }

        public int AffType { get; set; }

        public string PreferredLanguage { get; set; }
    }
}
