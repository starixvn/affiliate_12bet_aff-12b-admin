namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    using System;
    using System.Web.Mvc;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.Repository.UserPanel;
    using Filters;
    using MetalModels.UserPanel;
    using ONEbook.UM.Filters;
    using ViewModels.UserPanel;

    [CheckUMFilter]
    [CustomAuthorize]
    public class AccountOverviewController:Controller
    {
        private readonly ISessionService _sessionService;
        private readonly IStatisticsRepository _statisticsRepository;
        private readonly IAccountRepository _accountRepository;
        public AccountOverviewController(ISessionService sessionService, 
            IStatisticsRepository statisticsRepository,
            IAccountRepository accountRepository
            )
        {
            _sessionService = sessionService;
            _statisticsRepository = statisticsRepository;
            _accountRepository = accountRepository;
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult Index(DateTime? monthDate)
        {
            var param = new StatisticsOverviewParam
            {
                AffId = _sessionService.UserSession.AffId,
                MonthDate = monthDate.HasValue ? monthDate.Value : DateTime.Now.Date,
                IsInternal = _sessionService.IsInternal
            };

            var viewModel = new AccountOverviewViewModel
            {
                Statistics = _statisticsRepository.GetStatisticsOverview(param),
                CommissionSettingList = _accountRepository.GetCommissionSettingList(param.AffId, param.IsInternal),
                SelectedMonth = param.MonthDate
            };

            return View("~/Views/UserPanel/AccountOverView/index.cshtml", viewModel);
        }
    }
}