using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
using Fanex.Affiliate.Admin.Core.ViewModels.UserPanel;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    public class TrackingStatisticsController : BaseController
    {
        private readonly ITrackingStatisticsRepository _trackingStatisticsRepository;
        public TrackingStatisticsController(
            ISessionService sessionService,
            ITrackingStatisticsRepository trackingStatisticsRepository) : base(sessionService)
        {
            _trackingStatisticsRepository = trackingStatisticsRepository;
        }

        [HttpGet]
        public ActionResult Index(TrackingStatisticsParam param)
        {
            param.AffId = param.AffId == 0 ? SessionService.AffId : param.AffId;

            var trackingCodeList = _trackingStatisticsRepository.GetTrackingCodeList(param.AffId);

            var viewModel = new TrackingStatisticsViewModel
            {
                TrackingCodeList = trackingCodeList,
                TrackingStatisticsParam = param
            };

            return View("~/Areas/Portal/Views/UserPanel/TrackingStatistics/Index.cshtml", viewModel);
        }
    }
}
