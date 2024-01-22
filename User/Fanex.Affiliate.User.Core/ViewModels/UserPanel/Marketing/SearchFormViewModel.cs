using System.Collections.Generic;
using System.Web.Mvc;

namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel.Marketing
{
    public class SearchFormViewModel : LinkFormViewModel
    {
        public IEnumerable<SelectListItem> TargetProductList { get; set; }

        public IEnumerable<SelectListItem> SizeList { get; set; }
    }
}
