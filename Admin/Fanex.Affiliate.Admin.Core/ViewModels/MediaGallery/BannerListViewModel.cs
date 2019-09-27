using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Fanex.Affiliate.Admin.Core.ViewModels.MediaGallery
{
    public class BannerListViewModel
    {
        public IEnumerable<MediaGalleryFilterItem> CategoryList { get; set; }

        public IEnumerable<MediaGalleryFilterItem> SubCategoryCasinoList { get; set; }

        public IEnumerable<MediaGalleryFilterItem> SubCategorySportsbookList { get; set; }

        public IEnumerable<MediaGalleryFilterItem> ImageTypeList { get; set; }

        public IEnumerable<Language> LanguageList { get; set; }

        public IEnumerable<MediaGalleryFilterItem> CurrencyList { get; set; }

        public IEnumerable<MediaGalleryFilterItem> SizeList { get; set; }

        public IEnumerable<AffTypeInfo> AffTypeList { get; set; }

        [SuppressMessage("Sonar Code Smell", "S3996:UriPropertiesShouldNotBeStrings", Justification = "Reviewed. Suppression is OK here.")]
        public string BannerUrl { get; set; }
    }
}
