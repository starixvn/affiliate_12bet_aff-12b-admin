using System.Diagnostics.CodeAnalysis;

namespace Fanex.Affiliate.Admin.Core.ViewModels
{
    public class EmailTemplateViewModel
    {
        [SuppressMessage("Sonar Code Smell", "S3996:UriPropertiesShouldNotBeStrings", Justification = "Reviewed. Suppression is OK here.")]
        public string RootUrl { get; set; }

        [SuppressMessage("Sonar Code Smell", "S3996:UriPropertiesShouldNotBeStrings", Justification = "Reviewed. Suppression is OK here.")]
        public string UserSiteUrl { get; set; }

        [SuppressMessage("Sonar Code Smell", "S3996:UriPropertiesShouldNotBeStrings", Justification = "Reviewed. Suppression is OK here.")]
        public string ImageRootUrl { get; set; }

        public string Language { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
