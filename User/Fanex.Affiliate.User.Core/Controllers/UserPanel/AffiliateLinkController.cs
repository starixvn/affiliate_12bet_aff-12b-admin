namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    using System.Linq;
    using System.Web.Mvc;
    using BusinessService;
    using CoreService;
    using Enum;
    using Filters;
    using Resources;
    using Utilities;

    public class AffiliateLinkController : BaseMarketingController
    {
        public AffiliateLinkController(
            ISessionService sessionService,
            IAppSetting appSetting,
            ISystemService systemsService,
            IMarketingService marketingService,
            IAccountService accountService)
            : base(sessionService, appSetting, systemsService, marketingService, accountService)
        { }

        [HttpGet]
        [AjaxOnly]
        public ActionResult Index()
        {
            var affId = SessionService.UserSession.AffId;
            var targetCountryCode = AccountService.GetAccountInfo(affId)?.TargetCountryCode;

            var vm = GetLinkFormViewModel(targetCountryCode);
            vm.Channels = MarketingService.GetChannels(affId);

            if (!AccountService.GetGeneralAffInfo(affId).IsMaxSubLevel)
            {
                vm.LinkTypes = vm.LinkTypes
                    .Append(new SelectListItem
                    {
                        Value = AffiliateLinkType.SubAffiliateLink.ToString(),
                        Text = Lang.SubAffiliateLink
                    });
            }

            return View("~/Views/UserPanel/AccountOverview/AffiliateLink/Index.cshtml", vm);
        }
    }
}