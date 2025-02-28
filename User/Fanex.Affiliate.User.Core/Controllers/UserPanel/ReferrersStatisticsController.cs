namespace Fanex.Affiliate.User.Core.Controllers
{
    using Enum;
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
    public class ReferrersStatisticsController : BaseController
    {
        private readonly IStatisticsService _statisticsService;
        public ReferrersStatisticsController(ISessionService sessionService, ISystemService systemsService,
            IStatisticsService statisticsService
            ) : base(sessionService, systemsService)
        {
            _statisticsService = statisticsService;
        }


        [HttpGet]
        [AjaxOnly]
        public ActionResult Index()
        {
            var now = DateTimeOffset.Now.ToOffset(new TimeSpan(GMT.PlusEight, 0, 0));
            var referrerStatisticsParam = new ReferrerStatisticsParam
            {
                AffId = SessionService.UserSession.AffId,
                IsInternal = SessionService.IsInternal,
                FromDate = DateTimeHelper.CreateDateTime(now.Year, now.Month, 1),
                ToDate = now.DateTime,
                DeviceType = (byte)DeviceType.PCDevice
            };

            var viewModel = new ReferrerStatisticsViewModel
            {
                DataList = _statisticsService.GetReferrerStatistics(referrerStatisticsParam),
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