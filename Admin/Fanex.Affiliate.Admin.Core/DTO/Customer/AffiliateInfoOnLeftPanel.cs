using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.DTO.Customer
{
    public class AffiliateInfoOnLeftPanel
    {
        public int AffId { get; set; }

        public int GroupId { get; set; }

        public string AffName { get; set; }

        [CensoredPermissionCheck(Function.AffPersonalInfo)]
        public string FirstName { get; set; }

        [CensoredPermissionCheck(Function.AffPersonalInfo)]
        public string LastName { get; set; }

        public bool Gender { get; set; }

        [JsonIgnore]
        public string ContactApps { get; set; }

        [PermissionCheck(Function.AffWebUrl)]
        public string SiteUrl { get; set; }

        [PermissionCheck(Function.AffWebUrl)]
        public string MoreSiteUrl { get; set; }

        [PermissionCheck(Function.AffWebUrl)]
        public string NoSiteDescription { get; set; }

        [JsonIgnore]
        public DateTime? LastLoginDate { get; set; }

        public string LastLoginDateText => LastLoginDate.HasValue ? LastLoginDate.Value.ToString("MM/dd/yyyy HH:mm:ss") : string.Empty;

        public string LastLoginIp { get; set; }

        [PermissionCheck(Function.AssignCommGroup)]
        public string CommGroupName { get; set; }

        public decimal BalanceBf { get; set; }

        public string Status { get; set; }

        public bool IsClosed { get; set; }

        public int AffType { get; set; }

        [PermissionCheck(Function.LanguageAmendment)]
        public string PreferredLanguage { get; set; }

        [PermissionCheck(Function.AffPhone)]
        [CensoredPermissionCheck(Function.AffPersonalInfo)]
        public string Phone { get; set; }

        public string AffLink { get; set; }

        public IEnumerable<CensoredContactApp> ContactAppList { get; set; }
    }
}
