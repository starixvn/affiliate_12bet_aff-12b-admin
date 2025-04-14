namespace Fanex.Affiliate.Admin.Core.Controllers.Affiliate
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Attributes;
    using Constants;
    using DTO;
    using Resources;
    using Services.Common;
    using ViewModels.Affiliate;

    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class MarketingAccountController : ReadOnlySessionBaseController
    {
        private readonly ISystemService _systemService;

        public MarketingAccountController(
            ISessionService sessionService,
            ISystemService systemService) : base(sessionService)
        {
            _systemService = systemService;
        }

        public ActionResult Index()
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.AffiliateInfo, Url = Url.Content("~/Portal/Affiliate"), FunctionName = Function.Affiliate },
                new SubMenu { Name = Lang.PaymentRequest, Url = Url.Content("~/Portal/PaymentRequest"), FunctionName = Function.Payment },
                new SubMenu { Name = Lang.RevenueGroup, Url = Url.Content("~/Portal/RevenueGroup"), FunctionName = Function.RevenueGroup },
                new SubMenu { Name = Lang.EmailHistory, Url = Url.Content("~/Portal/EmailHistory"), FunctionName = Function.EmailHistory },
                new SubMenu { Name = Lang.MarketingAccount, Url = Url.Content("~/Portal/MarketingAccount"), FunctionName = Function.EmailHistory, IsActive = true } // TODO: Need Review
            };

            var viewModel = new MarketingAccountViewModel
            {
                LanguageList = _systemService.GetLanguageList(),
                TargetCountryList = _systemService.GetTargetCountryList(),
                SubMenuList = subMenu
            };

            return View("~/Areas/Portal/Views/Affiliate/MarketingAccount.cshtml", viewModel);
        }
    }
}
