namespace Fanex.Affiliate.User.Core.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Constants;
    using DTO;
    using DTO.UserPanel;
    using Enum;
    using Extensions;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.Repository.UserPanel;
    using Fanex.Affiliate.User.Core.Utilities;
    using Filters;
    using MetalModels.UserPanel;
    using ONEbook.UM.Filters;
    using ViewModels.UserPanel.Statistics;

    [CheckUMFilter]
    [CustomAuthorize]
    public class TrackingStatisticsController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly IStatisticsRepository _statisticsRepository;
        public TrackingStatisticsController(ISessionService sessionService,
            IStatisticsRepository statisticsRepository
            )
        {
            _sessionService = sessionService;
            _statisticsRepository = statisticsRepository;
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult Index()
        {
            var affId = _sessionService.UserSession.AffId;
            var isInternal = _sessionService.IsInternal;

            var viewModel = new TrackingStatisticsViewModel
            {
                TrackingCodeList = _statisticsRepository.GetTrackingCode(affId, isInternal).Where(c => !c.MatchWith("[None]")),
                TrackingStatisticsList = _statisticsRepository.GetTrackingStatistics(affId, null, isInternal),
                IsDetail = false
            };

            return View("~/Views/UserPanel/Statistics/TrackingStatistics/Index.cshtml", viewModel);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Index(string trackingCode)
        {
            var affId = _sessionService.UserSession.AffId;
            var isInternal = _sessionService.IsInternal;

            var viewModel = new TrackingStatisticsViewModel
            {
                TrackingStatisticsList = _statisticsRepository.GetTrackingStatistics(affId, trackingCode, isInternal),
                IsDetail = false
            };

            return PartialView("~/Views/UserPanel/Statistics/StatisticsPartial/_TrackingStatisticsContent.cshtml", viewModel);
        }


        [HttpPost]
        [AjaxOnly]
        public ActionResult TrackingStatisticsDetail(TrackingStatisticsDetailParam trackingStatisticsDetailParam)
        {
            trackingStatisticsDetailParam.AffId = _sessionService.UserSession.AffId;
            trackingStatisticsDetailParam.IsInternal = _sessionService.IsInternal;

            var viewModel = new TrackingStatisticsViewModel
            {
                TrackingStatisticsList = _statisticsRepository.GetTrackingStatisticsDetail(trackingStatisticsDetailParam),
                IsDetail = true
            };

            return PartialView("~/Views/UserPanel/Statistics/StatisticsPartial/_TrackingStatisticsContent.cshtml", viewModel);
        }
        
    }
}