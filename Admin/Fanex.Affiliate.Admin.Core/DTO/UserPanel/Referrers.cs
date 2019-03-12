using System.Diagnostics.CodeAnalysis;

namespace Fanex.Affiliate.Admin.Core.DTO.UserPanel
{
    public class Referrers
    {
        [SuppressMessage("Sonar Code Smell", "S3996:UriPropertiesShouldNotBeStrings", Justification = "Reviewed. Suppression is OK here.")]
        public string SiteUrl { get; set; }
        [SuppressMessage("Sonar Code Smell", "S3996:UriPropertiesShouldNotBeStrings", Justification = "Reviewed. Suppression is OK here.")]
        public string WebsiteUrl { get; set; }

        public int TotalClick { get; set; }

        public int UniqueClick { get; set; }

        public int Signups { get; set; }

        public int Fdm { get; set; }
    }
}
