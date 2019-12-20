namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    using System;
    using System.Web.Mvc;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.Repository.UserPanel;
    using Filters;
    using MetalModels.UserPanel;

    [CustomAuthorize]
    public class AccountOverviewController : BaseController
    {
        private readonly IAccountOverviewRepository _accountOverviewRepository;
        public AccountOverviewController(ISessionService sessionService,
            ILanguageService languageService,
            IAccountOverviewRepository accountOverviewRepository
            ) : base(sessionService, languageService)
        {
            _accountOverviewRepository = accountOverviewRepository;
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult Index()
        {
            return View("~/Views/UserPanel/AccountOverView/Index.cshtml");
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult Summary()
        {
            return PartialView("~/Views/UserPanel/AccountOverView/Summary/Index.cshtml");
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Dashboard(DateTime monthDate)
        {
            var model = _accountOverviewRepository.GetUserOverviewSummary(SessionService.UserSession.AffId, monthDate);
            return PartialView("~/Views/UserPanel/AccountOverView/Summary/Dashboard.cshtml", model);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Overview(DateTime monthDate)
        {
            var model = _accountOverviewRepository.GetUserOverviewDetailList(SessionService.UserSession.AffId, monthDate);
            return PartialView("~/Views/UserPanel/AccountOverView/Summary/Overview.cshtml", model);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult PlayersDetails(PlayerDetailParam param)
        {
            param.AffId = SessionService.UserSession.AffId;
            var model = _accountOverviewRepository.GetPlayerDetailList(param);
            return PartialView("~/Views/UserPanel/AccountOverView/Summary/PlayersDetails.cshtml", model);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult PlayersDetailsContent(PlayerDetailParam param)
        {
            param.AffId = SessionService.UserSession.AffId;
            var model = _accountOverviewRepository.GetPlayerDetailList(param);
            return PartialView("~/Views/UserPanel/AccountOverView/Summary/PlayersDetailsContent.cshtml", model);
        }
    }
}