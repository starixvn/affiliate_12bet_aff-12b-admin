namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    using BusinessService;
    using Constants;
    using CoreService;
    using Filters;
    using MetalModels.UserPanel;
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Utilities;
    using ViewModels.UserPanel.Statistics;

    [CustomAuthorize]
    public class MarketingLinkStatisticsController : BaseController
    {
        private readonly IStatisticsService _statisticsService;

        public MarketingLinkStatisticsController(
            ISessionService sessionService,
            ISystemService systemsService,
            IStatisticsService statisticsService
            ) : base(sessionService, systemsService)
        {
            _statisticsService = statisticsService;
        }

        public ActionResult Index()
        {
            var now = DateTimeHelper.Gmt8Now;
            var startMonth = DateTimeHelper.CreateDateTime(now.Year, now.Month, 1);
            var languages = SystemService.GetLanguageList().Select(l => new SelectListItem
            {
                Text = l.EnglishName,
                Value = l.Code
            });

            var viewModel = new MarketingPageStatisticsViewModel
            {
                FromDate = startMonth,
                ToDate = now,
                IncludeInActiveLink = false
            };

            ViewBag.Languages = languages;

            return View("~/Views/UserPanel/Statistics/MarketingPageStatistics/Index.cshtml", viewModel);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Index(MarketingPageStatisticsViewModel vm)
        {
            var param = new MarketingPageStatisticsParam
            {
                AffId = SessionService.UserSession.AffId,
                FromDate = vm.FromDate,
                ToDate = vm.ToDate,
                IncludedInActive = vm.IncludeInActiveLink,
                LanguageCodes = vm.Language
            };

            var model = _statisticsService.GetMarketingPageStatistics(param).GroupBy(s => s.ChannelName);

            return PartialView("~/Views/UserPanel/Statistics/StatisticsPartial/_MarketingPageStatisticsContent.cshtml", model);
        }
    }
}
