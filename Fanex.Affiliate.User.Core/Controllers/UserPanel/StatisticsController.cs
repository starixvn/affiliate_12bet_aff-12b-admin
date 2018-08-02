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
    public class StatisticsController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly IStatisticsRepository _statisticsRepository;
        public StatisticsController(ISessionService sessionService,
            IStatisticsRepository statisticsRepository
            )
        {
            _sessionService = sessionService;
            _statisticsRepository = statisticsRepository;
        }




        [HttpPost]
        [AjaxOnly]
        public ActionResult StatisticsDetailBySite(StatisticsDetailBySiteParam statisticsDetailParam)
        {
            statisticsDetailParam.AffId = _sessionService.UserSession.AffId;
            statisticsDetailParam.IsInternal = _sessionService.IsInternal;

            var viewModel = new ReferrerStatisticsViewModel
            {
                DataList = _statisticsRepository.GetStatisticsDetailBySite(statisticsDetailParam),
                IsDetail = true,
                RowNumber = statisticsDetailParam.NRows
            };

            return PartialView("~/Views/UserPanel/Statistics/StatisticsPartial/_ReferrersStatisticsContent.cshtml", viewModel);
        }

    }
}