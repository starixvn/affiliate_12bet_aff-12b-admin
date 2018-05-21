using System.Collections.Generic;
using System.Web.Mvc;

namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel.Marketing
{
    public class MarketingViewModel
    {
        public IEnumerable<SelectListItem> CategoryList { get; set; }

        public IEnumerable<SelectListItem> LanguageList { get; set; }

        public IEnumerable<SelectListItem> CurrencyList { get; set; }
    }
}
