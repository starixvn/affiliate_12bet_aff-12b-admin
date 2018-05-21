using System.Collections.Generic;
using System.Web.Mvc;

namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel.Marketing
{
    public class MediaGalleryViewModel : MarketingViewModel
    {
        public IEnumerable<SelectListItem> SubCategoryList { get; set; }

        public IEnumerable<SelectListItem> ImageTypeList { get; set; }

        public IEnumerable<SelectListItem> SizeList { get; set; }
    }
}
