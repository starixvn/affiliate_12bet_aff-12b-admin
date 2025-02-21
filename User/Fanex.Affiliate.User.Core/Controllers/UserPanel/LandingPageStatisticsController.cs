namespace Fanex.Affiliate.User.Core.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.Constants;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.DTO.UserPanel.Statistics;
    using Filters;
    using MetalModels.UserPanel;
    using Utilities;
    using ViewModels.UserPanel.Statistics;

    [CustomAuthorize]
    public class LandingPageStatisticsController : BaseController
    {
        private readonly IStatisticsService _statisticsService;
        private readonly ISystemService _systemService;
        private readonly IMarketingService _marketingService;

        public LandingPageStatisticsController(
            ISessionService sessionService,
            ISystemService systemService,
            IStatisticsService statisticsService,
            IMarketingService marketingService) : base(sessionService, systemService)
        {
            _statisticsService = statisticsService;
            _marketingService = marketingService;
            _systemService = systemService;
        }


        [HttpGet]
        [AjaxOnly]
        public ActionResult Index()
        {
            var userSession = SessionService.UserSession;
            var isInternal = SessionService.IsInternal;

            var now = DateTimeOffset.Now.ToOffset(new TimeSpan(GMT.PlusEight, 0, 0));
            var landingPageParam = new LandingPageStatisticsParam
            {
                AffId = userSession.AffId,
                IsInternal = isInternal,
                FromDate = DateTimeHelper.CreateDateTime(now.Year, now.Month, 1),
                ToDate = now.DateTime
            };

            var viewModel = new LandingPageStatisticsViewModel
            {
                LanguageList = MarketingHelper.GetLanguageList(_systemService.GetLanguageList(SessionService.IsInternal), userSession.PreferredLanguage),
                MemberSitePageList = _marketingService.GetLandingPageList(SessionService.UserSession.GroupName, isInternal),
                FromDate = landingPageParam.FromDate,
                ToDate = landingPageParam.ToDate,
                DataList = GetLandingPageStatisticsData(landingPageParam),
                IsDetail = false
            };

            return View("~/Views/UserPanel/Statistics/LandingPageStatistics/Index.cshtml", viewModel);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Index(LandingPageStatisticsParam landingPageParam)
        {
            landingPageParam.AffId = SessionService.UserSession.AffId;
            landingPageParam.IsInternal = SessionService.IsInternal;

            var selectedLanguage = landingPageParam.LanguageCode;
            if (!string.IsNullOrEmpty(selectedLanguage))
            {
                landingPageParam.LanguageCode = _systemService.GetLanguageList()
                    .FirstOrDefault(l => l.Code.Equals(selectedLanguage))?.ShortedCode
                                                ?? SystemLanguage.ShortedDefaultCode;
            }

            var viewModel = new LandingPageStatisticsViewModel
            {
                DataList = GetLandingPageStatisticsData(landingPageParam),
                IsDetail = false
            };

            return PartialView("~/Views/UserPanel/Statistics/StatisticsPartial/_LandingPageStatisticsContent.cshtml", viewModel);
        }

        private IEnumerable<LandingPageStatistics> GetLandingPageStatisticsData(LandingPageStatisticsParam landingPageParam)
        {
            var dataList = _statisticsService.GetLandingPageStatistics(landingPageParam);

            foreach (var item in dataList)
            {
                var language = _systemService.GetLanguageList().FirstOrDefault(p => p.ShortedCode == item.LanguageCode);

                item.LanguageName = language == null ? item.LanguageCode : language.NativeName;
            }

            return dataList;
        }
    }
}