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
    public class ReferrersStatisticsController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly IStatisticsRepository _statisticsRepository;
        public ReferrersStatisticsController(ISessionService sessionService,
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
            var now = DateTime.Now;
            var referrerStatisticsParam = new ReferrerStatisticsParam
            {
                AffId = _sessionService.UserSession.AffId,
                IsInternal = _sessionService.IsInternal,
                FromDate = new DateTime(now.Year, now.Month, 1),
                ToDate = now,
                DeviceType = (byte)DeviceType.PCDevice
            };

            var viewModel = new ReferrerStatisticsViewModel
            {
                DataList = _statisticsRepository.GetReferrerStatistics(referrerStatisticsParam),
                FromDate = referrerStatisticsParam.FromDate,
                ToDate = referrerStatisticsParam.ToDate
            };

            return View("~/Views/UserPanel/Statistics/ReferrersStatistics/Index.cshtml", viewModel);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Index(ReferrerStatisticsParam referrerStatisticsParam)
        {
            referrerStatisticsParam.AffId = _sessionService.UserSession.AffId;
            referrerStatisticsParam.IsInternal = _sessionService.IsInternal;

            var viewModel = new ReferrerStatisticsViewModel
            {
                DataList = _statisticsRepository.GetReferrerStatistics(referrerStatisticsParam)
            };

            return PartialView("~/Views/UserPanel/Statistics/StatisticsPartial/_ReferrersStatisticsContent.cshtml", viewModel);
        }


    }
}