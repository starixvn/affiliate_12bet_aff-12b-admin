using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class TrackingStatisticsController : BaseController
    {
        private readonly ITrackingStatisticsRepository _trackingStatisticsRepository;

        public TrackingStatisticsController(
            ISessionService sessionService,
            ITrackingStatisticsRepository trackingStatisticsRepository) : base(sessionService)
        {
            _trackingStatisticsRepository = trackingStatisticsRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult GetTrackingStatisticsList(TrackingStatisticsParam param)
        {
            if (ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = _trackingStatisticsRepository.GetTrackingStatisticsList(param)
                };
            }

            return new JsonNetResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult GetTrackingStatisticsDetailList(TrackingStatisticsDetailParam param)
        {
            if (ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = _trackingStatisticsRepository.GetTrackingStatisticsDetailList(param)
                };
            }

            return new JsonNetResult();
        }
    }
}
