namespace Fanex.Affiliate.User.Core.Controllers
{
    using System;
    using System.Web.Mvc;
    using Constants;
    using Extensions;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.Repository.UserPanel;
    using Fanex.Affiliate.User.Core.Utilities;
    using Filters;
    using MetalModels.UserPanel;
    using ONEbook.UM.Filters;
    using ViewModels.UserPanel;

    [CheckUMFilter]
    [CustomAuthorize]
    public class DailyEarningDetailController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly IStatementHistoryRepository _statementHistoryRepository;

        public DailyEarningDetailController(ISessionService sessionService,
            IStatementHistoryRepository statementHistoryRepository

            )
        {
            _sessionService = sessionService;
            _statementHistoryRepository = statementHistoryRepository;
        }


        [AjaxOnly]
        public ActionResult Index(DailyEarningParam param)
        {
            param.AffId = _sessionService.UserSession.AffId;
            param.IsInternal = _sessionService.IsInternal;

            var extraUserInfo = _statementHistoryRepository.GetExtraUserInfo(param.AffId, param.IsInternal);

            var viewModel = new DailyEarningDetailViewModel
            {
                MainData = _statementHistoryRepository.GetDailyEarningDetail(param),
                ProductList = param.IsOnlyContent ? null :
                _statementHistoryRepository.GetProductList(param.IsInternal),
                SelectedDate = param.MonthDate.HasValue ? param.MonthDate.Value : DateTime.Now.Date,
                SelectedProduct = param.SportType.HasValue ? param.SportType.Value : Product.Sportsbook,
                HasDailyDetail = extraUserInfo.HasDailyDetail
            };

            return param.IsOnlyContent ?
                                    View("~/Views/UserPanel/StatementHistory/StatementHistoryPartial/_DailyEarningDetailContent.cshtml", viewModel)
                                    : View("~/Views/UserPanel/StatementHistory/DailyEarningDetail.cshtml", viewModel);
        }
    }
}