namespace Fanex.Affiliate.User.Core.ViewModels.UserPanel
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Fanex.Affiliate.User.Core.DTO.UserPanel;

    public class MyAccountViewModel
    {
        public AccountInformation AccountInformation { get; set; }

        public IEnumerable<SelectListItem> BaseCurrencyList { get; set; }
    }
}
