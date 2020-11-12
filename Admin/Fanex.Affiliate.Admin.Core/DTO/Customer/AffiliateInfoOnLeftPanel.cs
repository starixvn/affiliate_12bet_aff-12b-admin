using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Fanex.Affiliate.Admin.Core.DTO.Customer
{
    public class AffiliateInfoOnLeftPanel
    {
        public int AffId { get; set; }

        public int GroupId { get; set; }

        public string AffName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Gender { get; set; }

        public string ContactApps { get; set; }

        [PermissionCheck(Function.AffWebUrl)]
        [SuppressMessage("Sonar Code Smell", "S3996:UriPropertiesShouldNotBeStrings", Justification = "Reviewed. Suppression is OK here.")]
        public string SiteUrl { get; set; }

        [PermissionCheck(Function.AffWebUrl)]
        [SuppressMessage("Sonar Code Smell", "S3996:UriPropertiesShouldNotBeStrings", Justification = "Reviewed. Suppression is OK here.")]
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
        public string Phone { get; set; }

        public string AffLink { get; set; }
    }
}
