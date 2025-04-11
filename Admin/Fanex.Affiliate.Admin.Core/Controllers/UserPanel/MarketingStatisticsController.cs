namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    using System.Linq;
    using System.Web.Mvc;
    using Attributes;
    using Constants;
    using Extensions;
    using MetaModels.UserPanel;
    using Services.Common;
    using Services.UserPanel.Statistics;

    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class MarketingStatisticsController : ReadOnlySessionBaseController
    {
        private readonly IMarketingStatisticsService _marketingStatisticsService;

        public MarketingStatisticsController(
            ISessionService sessionService,
            IMarketingStatisticsService marketingStatisticsService) : base(sessionService)
        {
            _marketingStatisticsService = marketingStatisticsService;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetMarketingStatisticsList(MarketingStatisticsParam param)
        {
            if (ModelState.IsValid)
            {
                return GenerateJsonNetResult(_marketingStatisticsService.GetMarketingStatistics(param).GroupBy(s=>s.ChannelName));
            }

            return new JsonNetResult();
        }
    }
}
