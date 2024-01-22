using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel.Marketing
{
    public class MediaGalleryViewModel
    {
        public SearchFormViewModel SearchForm { get; set; }

        public IEnumerable<Banner> MarketingDataList { get; set; }
    }
}