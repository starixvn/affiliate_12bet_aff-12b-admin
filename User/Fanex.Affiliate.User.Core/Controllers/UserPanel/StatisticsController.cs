namespace Fanex.Affiliate.User.Core.Controllers
{
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.CoreService;
    using Filters;
    using MetalModels.UserPanel;
    using System.Web.Mvc;
    using ViewModels.UserPanel.Statistics;

    [CustomAuthorize]
    public class StatisticsController : BaseController
    {
        private readonly IStatisticsService _statisticsService;
        public StatisticsController(
            ISessionService sessionService,
            ISystemService systemsService,
            IStatisticsService statisticsService
            ) : base(sessionService, systemsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult StatisticsDetailBySite(StatisticsDetailBySiteParam statisticsDetailParam)
        {
            statisticsDetailParam.AffId = SessionService.UserSession.AffId;
            statisticsDetailParam.IsInternal = SessionService.IsInternal;
            statisticsDetailParam.LanguageCode = _statisticsService.ConvertSelectedLanguageCodes(statisticsDetailParam.LanguageCode);

            var viewModel = new ReferrerStatisticsViewModel
            {
                DataList = _statisticsService.GetStatisticsDetailBySite(statisticsDetailParam),
                IsDetail = true,
                RowNumber = statisticsDetailParam.NRows
            };

            return PartialView("~/Views/UserPanel/Statistics/StatisticsPartial/_ReferrersStatisticsContent.cshtml", viewModel);
        }
    }
}