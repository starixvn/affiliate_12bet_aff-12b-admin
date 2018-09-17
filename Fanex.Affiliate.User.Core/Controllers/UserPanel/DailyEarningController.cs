namespace Fanex.Affiliate.User.Core.Controllers
{
    using System;
    using System.Web.Mvc;
    using Constants;
    using Extensions;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.Repository.UserPanel;
    using Filters;
    using MetalModels.UserPanel;
    using ViewModels.UserPanel;

    [CustomAuthorize]
    public class DailyEarningController : BaseController
    {
        private readonly IStatementHistoryRepository _statementHistoryRepository;

        public DailyEarningController(ISessionService sessionService,
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
            param.ProductId = param.ProductId.HasValue ? param.ProductId.Value : Product.Sportsbook;
            param.MonthDate = param.MonthDate.HasValue ? param.MonthDate.Value : DateTime.Now.AddDays(-1);

            var extraUserInfo = _statementHistoryRepository.GetExtraUserInfo(param.AffId, param.IsInternal);

            var viewModel = new DailyEarningViewModel
            {
                ProductList = param.IsOnlyContent ? null : _statementHistoryRepository.GetProductList(param.IsInternal),
                Data = _statementHistoryRepository.GetDailyEarning(param),
                SelectedProduct = param.ProductId.Value,
                IsKoreanAgentCommGroup = !string.IsNullOrEmpty(extraUserInfo.GroupName) && extraUserInfo.GroupName.MatchWith(CommGroupName.KoreanAgentComm),
                HasDailyDetail = extraUserInfo.HasDailyDetail
            };
            ViewBag.MonthDate = param.MonthDate;
            var viewPath = param.IsOnlyContent ? "~/Views/UserPanel/StatementHistory/StatementHistoryPartial/_DailyEarningContent.cshtml" :
                "~/Views/UserPanel/StatementHistory/DailyEarning.cshtml";

            return View(viewPath, viewModel);

        }

    }
}