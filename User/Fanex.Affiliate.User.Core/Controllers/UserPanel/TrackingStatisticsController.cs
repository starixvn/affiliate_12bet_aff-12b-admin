namespace Fanex.Affiliate.User.Core.Controllers
{
    using Extensions;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.CoreService;
    using Filters;
    using MetalModels.UserPanel;
    using System.Linq;
    using System.Web.Mvc;
    using ViewModels.UserPanel.Statistics;

    [CustomAuthorize]
    public class TrackingStatisticsController : BaseController
    {
        private readonly IStatisticsService _statisticsService;
        public TrackingStatisticsController(ISessionService sessionService,
            ILanguageService languageService,
            IStatisticsService statisticsService
            ) : base(sessionService, languageService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult Index()
        {
            var affId = SessionService.UserSession.AffId;
            var isInternal = SessionService.IsInternal;

            var viewModel = new TrackingStatisticsViewModel
            {
                TrackingCodeList = _statisticsService.GetTrackingCode(affId, isInternal).Where(c => !c.MatchWith("[None]")),
                TrackingStatisticsList = _statisticsService.GetTrackingStatistics(affId, null, isInternal),
                CountryList = _statisticsService.GetCountryList(isInternal),
                IsDetail = false,
                CurrentLanguage = SessionService.CurrentLanguage
            };

            return View("~/Views/UserPanel/Statistics/TrackingStatistics/Index.cshtml", viewModel);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Index(string trackingCode)
        {
            var affId = SessionService.UserSession.AffId;
            var isInternal = SessionService.IsInternal;

            var viewModel = new TrackingStatisticsViewModel
            {
                TrackingStatisticsList = _statisticsService.GetTrackingStatistics(affId, trackingCode, isInternal),
                IsDetail = false,
                CurrentLanguage = SessionService.CurrentLanguage
            };

            return PartialView("~/Views/UserPanel/Statistics/StatisticsPartial/_TrackingStatisticsContent.cshtml", viewModel);
        }


        [HttpPost]
        [AjaxOnly]
        public ActionResult TrackingStatisticsDetail(TrackingStatisticsDetailParam trackingStatisticsDetailParam)
        {
            trackingStatisticsDetailParam.AffId = SessionService.UserSession.AffId;
            trackingStatisticsDetailParam.IsInternal = SessionService.IsInternal;

            var viewModel = new TrackingStatisticsViewModel
            {
                TrackingStatisticsList = _statisticsService.GetTrackingStatisticsDetail(trackingStatisticsDetailParam),
                IsDetail = true
            };

            return PartialView("~/Views/UserPanel/Statistics/StatisticsPartial/_TrackingStatisticsContent.cshtml", viewModel);
        }
    }
}