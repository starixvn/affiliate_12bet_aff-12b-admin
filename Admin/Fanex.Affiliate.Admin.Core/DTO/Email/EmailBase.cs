using System.Diagnostics.CodeAnalysis;

namespace Fanex.Affiliate.Admin.Core.DTO.Email
{
    public class EmailBase
    {
        [SuppressMessage("Sonar Code Smell", "S3996:UriPropertiesShouldNotBeStrings", Justification = "Reviewed. Suppression is OK here.")]
        public string RootUrl { get; set; }

        [SuppressMessage("Sonar Code Smell", "S3996:UriPropertiesShouldNotBeStrings", Justification = "Reviewed. Suppression is OK here.")]
        public string UserSiteUrl { get; set; }

        public string MailContact { get; set; }

        public string Language { get; set; }

        public string MailTo { get; set; }

        public bool IsHtml { get; set; }
    }
}