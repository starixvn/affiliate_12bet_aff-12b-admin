namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.Constants;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.Filters;
    using Fanex.Affiliate.User.Core.ViewModels.UserPanel.Statistics;
    using MetalModels.UserPanel;

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
            var now = DateTimeOffset.Now.ToOffset(new TimeSpan(GMT.PlusEight, 0, 0));
            var startMonth = new DateTime(now.Year, now.Month, 1);
            var languages = SystemService.GetLanguageList(SessionService.IsInternal).Select(l => new SelectListItem
            {
                Text = l.EnglishName,
                Value = l.Code
            });

            var viewModel = new MarketingPageStatisticsViewModel
            {
                FromDate = startMonth,
                ToDate = now.DateTime,
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
