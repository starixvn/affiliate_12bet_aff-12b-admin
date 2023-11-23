namespace Fanex.Affiliate.User.Core.Controllers
{
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.Constants;
    using Fanex.Affiliate.User.Core.CoreService;
    using Filters;
    using MetalModels.UserPanel;
    using System;
    using System.Web.Mvc;
    using Utilities;
    using ViewModels.UserPanel.Statistics;

    [CustomAuthorize]
    public class LandingPageStatisticsController : BaseController
    {
        private readonly IStatisticsService _statisticsService;

        public LandingPageStatisticsController(
            ISessionService sessionService,
            ISystemService systemService,
            IStatisticsService statisticsService
            ) : base(sessionService, systemService)
        {
            _statisticsService = statisticsService;
        }


        [HttpGet]
        [AjaxOnly]
        public ActionResult Index()
        {
            var userSession = SessionService.UserSession;
            var isInternal = SessionService.IsInternal;

            var now = DateTime.Now;
            var landingPageParam = new LandingPageStatisticsParam
            {
                AffId = userSession.AffId,
                ReportBy = Statistics.ReportByCountry,
                CountryCodes = _statisticsService.GetCountryCodes(isInternal),
                IsInternal = isInternal,
                FromDate = new DateTime(now.Year, now.Month, 1),
                ToDate = now
            };

            var viewModel = new LandingPageStatisticsViewModel
            {
                LanguageList = SystemHelper.GetLanguageList(SystemService.GetSystemLanguages(), userSession.PreferredLanguage),
                MemberSitePageList = _statisticsService.GetLandingPageList(userSession.GroupName, isInternal),
                CountryList = _statisticsService.GetCountryList(isInternal),
                ReportBy = Statistics.ReportByCountry,
                FromDate = landingPageParam.FromDate,
                ToDate = landingPageParam.ToDate,
                DataList = _statisticsService.GetLandingPageStatistics(landingPageParam)
            };

            return View("~/Views/UserPanel/Statistics/LandingPageStatistics/Index.cshtml", viewModel);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Index(LandingPageStatisticsParam landingPageParam)
        {
            landingPageParam.AffId = SessionService.UserSession.AffId;
            landingPageParam.IsInternal = SessionService.IsInternal;

            var viewModel = new LandingPageStatisticsViewModel
            {
                ReportBy = landingPageParam.ReportBy,
                DataList = _statisticsService.GetLandingPageStatistics(landingPageParam)
            };

            return PartialView("~/Views/UserPanel/Statistics/StatisticsPartial/_LandingPageStatisticsContent.cshtml", viewModel);
        }
    }
}