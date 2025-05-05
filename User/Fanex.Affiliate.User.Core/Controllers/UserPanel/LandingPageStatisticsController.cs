namespace Fanex.Affiliate.User.Core.Controllers
{
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.Constants;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.DTO.UserPanel.Statistics;
    using Fanex.Affiliate.User.Core.Enum;
    using Fanex.Affiliate.User.Core.Extensions;
    using Filters;
    using MetalModels.UserPanel;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
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
                ToDate = now.DateTime,
                DeviceType = (short)DeviceTypeHasAll.All
            };

            var viewModel = new LandingPageStatisticsViewModel
            {
                LanguageList = SystemHelper.GetTargetLanguageList(_systemService.GetLanguageList(), userSession.PreferredLanguage),
                MemberSitePageList = _marketingService.GetLandingPageList(SessionService.UserSession.GroupName, isInternal, language: SystemLanguage.IndiaLanguageCode),
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

            var viewModel = new LandingPageStatisticsViewModel
            {
                DataList = GetLandingPageStatisticsData(landingPageParam),
                IsDetail = false
            };

            return PartialView("~/Views/UserPanel/Statistics/StatisticsPartial/_LandingPageStatisticsContent.cshtml", viewModel);
        }

        private IEnumerable<LandingPageStatistics> GetLandingPageStatisticsData(LandingPageStatisticsParam landingPageParam)
        {
            landingPageParam.LanguageCode = ConvertSelectedShortedCodes(landingPageParam.LanguageCode);
            var dataList = _statisticsService.GetLandingPageStatistics(landingPageParam);

            foreach (var item in dataList)
            {
                var language = _systemService.GetLanguageList().FirstOrDefault(p => p.ShortedCode == item.LanguageCode);

                item.LanguageName = language == null ? item.LanguageCode : language.NativeName;
            }

            return dataList;
        }

        private string ConvertSelectedShortedCodes(string languageCode)
        {
            if (string.IsNullOrEmpty(languageCode))
            {
                return languageCode;
            }

            var allLanguages = _systemService.GetLanguageList();

            return allLanguages.FirstOrDefault(l => l.Code.MatchWith(languageCode))?.ShortedCode ?? SystemLanguage.ShortedDefaultCode;
        }
    }
}