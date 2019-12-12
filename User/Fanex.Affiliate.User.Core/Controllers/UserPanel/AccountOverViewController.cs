namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    using System;
    using System.Web.Mvc;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.Repository.UserPanel;
    using Filters;
    using MetalModels.UserPanel;
    using ViewModels.UserPanel;

    [CustomAuthorize]
    public class AccountOverviewController : BaseController
    {
        private readonly IStatisticsRepository _statisticsRepository;
        private readonly IAccountRepository _accountRepository;
        public AccountOverviewController(ISessionService sessionService,
            ILanguageService languageService,
            IStatisticsRepository statisticsRepository,
            IAccountRepository accountRepository
            ) : base(sessionService, languageService)
        {
            _statisticsRepository = statisticsRepository;
            _accountRepository = accountRepository;
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult Index(DateTime? monthDate)
        {
            var param = new StatisticsOverviewParam
            {
                AffId = SessionService.UserSession.AffId,
                MonthDate = monthDate.HasValue ? monthDate.Value : DateTime.Now.Date,
                IsInternal = SessionService.IsInternal
            };

            var viewModel = new AccountOverviewViewModel
            {
                Statistics = _statisticsRepository.GetStatisticsOverview(param),
                CommissionSettingList = _accountRepository.GetCommissionSettingList(param.AffId, param.IsInternal),
                SelectedMonth = param.MonthDate
            };

            return View("~/Views/UserPanel/AccountOverView/Index.cshtml", viewModel);
        }

        [HttpPost]
        public ActionResult Summary()
        {
            return PartialView("~/Views/UserPanel/AccountOverView/Summary/Index.cshtml");
        }

        [HttpPost]
        public ActionResult Dashboard(DateTime? monthDate)
        {
            return PartialView("~/Views/UserPanel/AccountOverView/Summary/Dashboard.cshtml");
        }

        [HttpPost]
        public ActionResult Overview(DateTime? monthDate)
        {
            return PartialView("~/Views/UserPanel/AccountOverView/Summary/Overview.cshtml");
        }

        [HttpPost]
        public ActionResult PlayersDetails(DateTime? monthDate)
        {
            return PartialView("~/Views/UserPanel/AccountOverView/Summary/PlayersDetails.cshtml");
        }
    }
}