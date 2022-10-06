namespace Fanex.Affiliate.User.Core.Controllers
{
    using Enum;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.CoreService;
    using Filters;
    using MetalModels.UserPanel;
    using System;
    using System.Web.Mvc;
    using ViewModels.UserPanel.Statistics;

    [CustomAuthorize]
    public class ReferrersStatisticsController : BaseController
    {
        private readonly IStatisticsService _statisticsService;
        public ReferrersStatisticsController(ISessionService sessionService, ILanguageService languageService,
            IStatisticsService statisticsService
            ) : base(sessionService, languageService)
        {
            _statisticsService = statisticsService;
        }


        [HttpGet]
        [AjaxOnly]
        public ActionResult Index()
        {
            var now = DateTime.Now;
            var isInternal = SessionService.IsInternal;

            var referrerStatisticsParam = new ReferrerStatisticsParam
            {
                AffId = SessionService.UserSession.AffId,
                IsInternal = isInternal,
                FromDate = new DateTime(now.Year, now.Month, 1),
                ToDate = now,
                CountryCodes = _statisticsService.GetCountryCodes(isInternal),
                DeviceType = (byte)DeviceType.PCDevice
            };

            var viewModel = new ReferrerStatisticsViewModel
            {
                DataList = _statisticsService.GetReferrerStatistics(referrerStatisticsParam),
                CountryList = _statisticsService.GetCountryList(isInternal),
                FromDate = referrerStatisticsParam.FromDate,
                ToDate = referrerStatisticsParam.ToDate
            };

            return View("~/Views/UserPanel/Statistics/ReferrersStatistics/Index.cshtml", viewModel);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Index(ReferrerStatisticsParam referrerStatisticsParam)
        {
            referrerStatisticsParam.AffId = SessionService.UserSession.AffId;
            referrerStatisticsParam.IsInternal = SessionService.IsInternal;

            var viewModel = new ReferrerStatisticsViewModel
            {
                DataList = _statisticsService.GetReferrerStatistics(referrerStatisticsParam)
            };

            return PartialView("~/Views/UserPanel/Statistics/StatisticsPartial/_ReferrersStatisticsContent.cshtml", viewModel);
        }
    }
}