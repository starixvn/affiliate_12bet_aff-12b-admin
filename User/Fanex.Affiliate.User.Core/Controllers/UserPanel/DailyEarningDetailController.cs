namespace Fanex.Affiliate.User.Core.Controllers
{
    using System;
    using System.Web.Mvc;
    using Constants;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.Repository.UserPanel;
    using Filters;
    using MetalModels.UserPanel;
    using ViewModels.UserPanel;

    [CustomAuthorize]
    public class DailyEarningDetailController : BaseController
    {
        private readonly IStatementHistoryRepository _statementHistoryRepository;

        public DailyEarningDetailController(ISessionService sessionService, 
            ILanguageService languageService,
            IStatementHistoryRepository statementHistoryRepository

            ) : base(sessionService, languageService)
        {
            _statementHistoryRepository = statementHistoryRepository;
        }


        [AjaxOnly]
        public ActionResult Index(DailyEarningParam param)
        {
            param.AffId = SessionService.UserSession.AffId;
            param.IsInternal = SessionService.IsInternal;

            var extraUserInfo = _statementHistoryRepository.GetExtraUserInfo(param.AffId, param.IsInternal);

            var viewModel = new DailyEarningDetailViewModel
            {
                MainData = _statementHistoryRepository.GetDailyEarningDetail(param),
                ProductList = param.IsOnlyContent ? null :
                _statementHistoryRepository.GetProductList(param.IsInternal),
                SelectedDate = param.MonthDate.HasValue ? param.MonthDate.Value : DateTime.Now.Date,
                SelectedProduct = param.ProductId.HasValue ? param.ProductId.Value : Product.Sportsbook,
                HasDailyDetail = extraUserInfo.HasDailyDetail
            };

            return param.IsOnlyContent ?
                                    View("~/Views/UserPanel/StatementHistory/StatementHistoryPartial/_DailyEarningDetailContent.cshtml", viewModel)
                                    : View("~/Views/UserPanel/StatementHistory/DailyEarningDetail.cshtml", viewModel);
        }
    }
}