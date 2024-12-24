namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using BusinessService;
    using CoreService;
    using Filters;
    using ONEbook.UM.AspNetMVC.Filters;
    using Utilities;
    using ViewModels.UserPanel.Marketing;

    [CheckUMFilter]
    [CustomAuthorize]
    public class AffiliateLinkController : BaseMarketingController
    {
        private readonly IAccountService _accountService;

        public AffiliateLinkController(
            ISessionService sessionService,
            IAppSetting appSetting,
            ISystemService systemsService,
            IMarketingService marketingService,
            IAccountService accountService)
            : base(sessionService, appSetting, systemsService, marketingService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult Index()
        {
            var affId = SessionService.UserSession.AffId;
            var langs = SystemService.GetLanguageList(SessionService.IsInternal);
            var channels = MarketingService.GetChannels(affId);

            var refs = new List<RefLinkViewModel>
            {
                GetAffLinkTypeViewModel(),
            };

            if (!_accountService.GetGeneralAffInfo(affId).IsMaxSubLevel)
            {
                refs.Add(GetSubAffLinkTypeViewModel());
            }

            var vm = new AffiliateLinkViewModel
            {
                Channels = channels,
                Languages = langs,
                RefLinks = refs
            };

            ViewBag.SelectedLang = SessionService.UserSession.PreferredLanguage;

            return View("~/Views/UserPanel/AccountOverview/AffiliateLink/Index.cshtml", vm);
        }
    }
}