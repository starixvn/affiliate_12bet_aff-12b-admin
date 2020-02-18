namespace Fanex.Affiliate.User.Core.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Constants;
    using DTO.UserPanel;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.Repository.UserPanel;
    using Filters;
    using MetalModels.UserPanel;
    using Utilities;
    using ViewModels.UserPanel.Statistics;

    [CustomAuthorize]
    public class LandingPageStatisticsController : BaseController
    {
        private readonly IStatisticsRepository _statisticsRepository;
        private readonly IMarketingService _marketingService;

        public LandingPageStatisticsController(ISessionService sessionService,
            ILanguageService languageService,
            IStatisticsRepository statisticsRepository,
            IMarketingService marketingService
            ) : base(sessionService, languageService)
        {
            _statisticsRepository = statisticsRepository;
            _marketingService = marketingService;
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
                IsInternal = isInternal,
                FromDate = new DateTime(now.Year, now.Month, 1),
                ToDate = now
            };

            var viewModel = new LandingPageStatisticsViewModel
            {
                LanguageList = MarketingHelper.GetLanguageList(_marketingService.GetLanguageList(SessionService.IsInternal), userSession.PreferredLanguage),
                MemberSitePageList = _marketingService.GetLandingPageList(isInternal),
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
                landingPageParam.LanguageCode = SystemLanguage.LanguageList
                                                .FirstOrDefault(l => l.Code.Equals(selectedLanguage))
                                                ?.ShortedCode ?? SystemLanguage.ShortedDefaultCode;
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
            var dataList = _statisticsRepository.GetLandingPageStatistics(landingPageParam);

            foreach (var item in dataList)
            {
                var language = SystemLanguage.LanguageList.FirstOrDefault(p => p.ShortedCode == item.LanguageCode);

                item.LanguageName = language == null ? item.LanguageCode : language.NativeName;
            }

            return dataList;
        }
    }
}