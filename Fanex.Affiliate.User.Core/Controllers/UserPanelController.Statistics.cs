namespace Fanex.Affiliate.User.Core.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Enum;
    using Extensions;
    using Filters;
    using MetalModels.UserPanel;
    using ViewModels.UserPanel.Statistics;

    public partial class UserPanelController
    {
        [HttpGet]
        [AjaxOnly]
        public ActionResult TrackingStatistics()
        {
            var affId = SessionService.UserSession.AffId;
            var isInternal = SessionService.IsInternal;

            var viewModel = new TrackingStatisticsViewModel
            {
                TrackingCodeList = _statisticsRepository.GetTrackingCode(affId, isInternal).Where(c => !c.MatchWith("[None]")),
                TrackingStatisticsList = _statisticsRepository.GetTrackingStatistics(affId, null, isInternal),
                IsDetail = false
            };

            return View(viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetTrackingStatistics(string trackingCode)
        {
            var affId = SessionService.UserSession.AffId;
            var isInternal = SessionService.IsInternal;

            var viewModel = new TrackingStatisticsViewModel
            {
                TrackingStatisticsList = _statisticsRepository.GetTrackingStatistics(affId, trackingCode, isInternal),
                IsDetail = false
            };

            return PartialView("~/Views/UserPanel/Statistics/_TrackingStatisticsContent.cshtml", viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetTrackingStatisticsDetail(TrackingStatisticsDetailParam trackingStatisticsDetailParam)
        {
            trackingStatisticsDetailParam.AffId = SessionService.UserSession.AffId;
            trackingStatisticsDetailParam.IsInternal = SessionService.IsInternal;

            var viewModel = new TrackingStatisticsViewModel
            {
                TrackingStatisticsList = _statisticsRepository.GetTrackingStatisticsDetail(trackingStatisticsDetailParam),
                IsDetail = true
            };

            return PartialView("~/Views/UserPanel/Statistics/_TrackingStatisticsContent.cshtml", viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult ReferrersStatistics()
        {
            var now = DateTime.Now;
            var referrerStatisticsParam = new ReferrerStatisticsParam
            {
                AffId = SessionService.UserSession.AffId,
                IsInternal = SessionService.IsInternal,
                FromDate = new DateTime(now.Year, now.Month, 1),
                ToDate = now,
                DeviceType = (byte)DeviceType.PCDevice
            };

            var viewModel = new ReferrerStatisticsViewModel
            {
                DataList = _statisticsRepository.GetReferrerStatistics(referrerStatisticsParam)
            };

            return View(viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetReferrerStatistics(ReferrerStatisticsParam referrerStatisticsParam)
        {
            referrerStatisticsParam.AffId = SessionService.UserSession.AffId;
            referrerStatisticsParam.IsInternal = SessionService.IsInternal;

            var viewModel = new ReferrerStatisticsViewModel
            {
                DataList = _statisticsRepository.GetReferrerStatistics(referrerStatisticsParam)
            };

            return PartialView("~/Views/UserPanel/Statistics/_ReferrersStatisticsContent.cshtml", viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetStatisticsDetailBySite(StatisticsDetailBySiteParam statisticsDetailParam)
        {
            statisticsDetailParam.AffId = SessionService.UserSession.AffId;
            statisticsDetailParam.IsInternal = SessionService.IsInternal;

            var viewModel = new ReferrerStatisticsViewModel
            {
                DataList = _statisticsRepository.GetStatisticsDetailBySite(statisticsDetailParam),
                IsDetail = true,
                RowNumber = statisticsDetailParam.NRows
            };

            return PartialView("~/Views/UserPanel/Statistics/_ReferrersStatisticsContent.cshtml", viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult LandingPageStatistics()
        {
            return View();
        }
    }
}