namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel.Marketing
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class MediaGallerySearchFormViewModel : CommonSearchFormViewModel
    {
        public IEnumerable<SelectListItem> CurrencyList { get; set; }

        public IEnumerable<SelectListItem> DeviceList { get; set; }
    }
}