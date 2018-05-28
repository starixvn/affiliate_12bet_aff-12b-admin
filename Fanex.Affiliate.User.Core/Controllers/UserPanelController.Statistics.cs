namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
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
            return View();
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult LandingPageStatistics()
        {
            return View();
        }
    }
}