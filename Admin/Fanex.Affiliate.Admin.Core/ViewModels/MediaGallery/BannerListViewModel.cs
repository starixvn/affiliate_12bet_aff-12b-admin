using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.ViewModels.MediaGallery
{
    public class BannerListViewModel
    {
        public IEnumerable<Category> CategoryList { get; set; }

        public IEnumerable<Language> LanguageList { get; set; }

        public IEnumerable<MediaGalleryFilterItem> SizeList { get; set; }

        public IEnumerable<AffType> AffTypeList { get; set; }

        public BannerParamFromHome Param { get; set; }

        public string BannerRootUrl { get; set; }
    }
}
