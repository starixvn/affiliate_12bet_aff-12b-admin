namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.Repository.UserPanel;
    using Filters;
    using MetalModels.UserPanel;
    using ViewModels.UserPanel.Statistics;

    [CustomAuthorize]
    public class StatisticsController : BaseController
    {
        private readonly IStatisticsRepository _statisticsRepository;
        public StatisticsController(ISessionService sessionService, ILanguageService languageService,
            IStatisticsRepository statisticsRepository
            ) : base(sessionService, languageService)
        {
            _statisticsRepository = statisticsRepository;
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult StatisticsDetailBySite(StatisticsDetailBySiteParam statisticsDetailParam)
        {
            statisticsDetailParam.AffId = SessionService.UserSession.AffId;
            statisticsDetailParam.IsInternal = SessionService.IsInternal;

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