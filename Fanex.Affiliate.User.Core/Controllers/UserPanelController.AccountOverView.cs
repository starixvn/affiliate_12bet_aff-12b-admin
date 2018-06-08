namespace Fanex.Affiliate.User.Core.Controllers
{
    using System;
    using System.Web.Mvc;
    using Filters;
    using MetalModels.UserPanel;
    using ViewModels.UserPanel;

    public partial class UserPanelController
    {
        [HttpGet]
        [AjaxOnly]
        public ActionResult AccountOverview(DateTime? monthDate)
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
                CommissionSettingList = _commissionRepository.GetCommissionSettingList(param.AffId, param.IsInternal),
                SelectedMonth = param.MonthDate
            };

            return View(viewModel);
        }
    }
}