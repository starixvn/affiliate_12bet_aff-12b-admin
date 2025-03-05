namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel
{
    using Fanex.Affiliate.User.Core.DTO.UserPanel;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class MyAccountViewModel
    {
        public AccountInformation AccountInformation { get; set; }

        public IEnumerable<SelectListItem> BaseCurrencyList { get; set; }

        public bool IsInternal { get; set; }
    }
}
