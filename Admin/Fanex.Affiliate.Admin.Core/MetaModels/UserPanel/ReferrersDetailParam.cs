using System.Diagnostics.CodeAnalysis;

namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class ReferrersDetailParam : ReferrersParam
    {
        [SuppressMessage("Sonar Code Smell", "S3996:UriPropertiesShouldNotBeStrings", Justification = "Reviewed. Suppression is OK here.")]
        public string SelectedSiteUrl { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
