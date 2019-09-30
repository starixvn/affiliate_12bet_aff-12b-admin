using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Fanex.Affiliate.Admin.Core.ViewModels.MediaGallery
{
    public class BannerListViewModel
    {
        public IEnumerable<MediaGalleryFilterItem> CategoryList { get; set; }

        public IEnumerable<MediaGalleryFilterItem> ImageTypeList { get; set; }

        public IEnumerable<Language> LanguageList { get; set; }

        public IEnumerable<MediaGalleryFilterItem> CurrencyList { get; set; }

        public IEnumerable<MediaGalleryFilterItem> SizeList { get; set; }

        public IEnumerable<AffTypeInfo> AffTypeList { get; set; }
    }
}
