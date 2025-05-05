namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel.Marketing
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class CommonSearchFormViewModel : AffiliateLinkViewModel
    {
        public IEnumerable<SelectListItem> CategoryList { get; set; }

        public IEnumerable<SelectListItem> SizeList {get; set; }
    }
}
