namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel.Marketing
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class AffiliateLinkViewModel
    {
        public IEnumerable<SelectListItem> LinkTypes { get; set; }

        public IEnumerable<SelectListItem> TargetCountries { get; set; }

        public IEnumerable<SelectListItem> Domains { get; set; }

        public IEnumerable<SelectListItem> SitePages { get; set; }

        public IEnumerable<SelectListItem> Languages { get; set; }

        public IDictionary<string, string> Channels { get; set; }
    }
}
