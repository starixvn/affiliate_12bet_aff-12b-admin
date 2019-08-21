using System.Diagnostics.CodeAnalysis;

namespace Fanex.Affiliate.Admin.Core.DTO
{
    public class SubMenu
    {
        public string Name { get; set; }

        [SuppressMessage("Sonar Code Smell", "S3996:UriPropertiesShouldNotBeStrings", Justification = "Reviewed. Suppression is OK here.")]
        public string Url { get; set; }

        public bool IsActive { get; set; }
    }
}