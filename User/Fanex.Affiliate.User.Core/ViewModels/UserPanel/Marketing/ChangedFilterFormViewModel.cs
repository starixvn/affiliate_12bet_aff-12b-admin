namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel.Marketing
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class ChangedFilterFormViewModel
    {
        public IEnumerable<SelectListItem> Domains { get; set; }

        public IEnumerable<SelectListItem> SitePages { get; set; }

        public IEnumerable<SelectListItem> Languages { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; }

        public IEnumerable<SelectListItem> SizeList { get; set; }
    }
}
