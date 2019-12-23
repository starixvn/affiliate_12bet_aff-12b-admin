using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.Filters;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using System;
using System.Web.Mvc;

namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    [CustomAuthorize]
    public class SummaryController : BaseController
    {
        private readonly IAccountOverviewRepository _accountOverviewRepository;

        public SummaryController(ISessionService sessionService,
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

            ViewBag.TotalItem = param.NRows;

            return PartialView("~/Views/UserPanel/AccountOverView/Summary/PlayersDetails.cshtml", model);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult PlayersDetailsContent(PlayerDetailParam param)
        {
            param.AffId = SessionService.UserSession.AffId;
            var model = _accountOverviewRepository.GetPlayerDetailList(param);

            ViewBag.TotalItem = param.NRows;

            return PartialView("~/Views/UserPanel/AccountOverView/Summary/PlayersDetailsContent.cshtml", model);
        }
    }
}
