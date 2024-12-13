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
    public class DailyEarningController : BaseController
    {
        private readonly IStatementHistoryRepository _statementHistoryRepository;
        private readonly ICurrencyRepository _currencyRepository;

        public DailyEarningController(
            ISessionService sessionService,
            ISystemService languageService,
            IStatementHistoryRepository statementHistoryRepository,
            ICurrencyRepository currencyRepository) : base(sessionService, languageService)
        {
            _statementHistoryRepository = statementHistoryRepository;
            _currencyRepository = currencyRepository;
        }

        [AjaxOnly]
        public ActionResult Index(DailyEarningParam param)
        {
            param.AffId = SessionService.UserSession.AffId;
            param.IsInternal = SessionService.IsInternal;
            param.ProductId = param.ProductId ?? Product.Sportsbook;
            param.MonthDate = param.MonthDate ?? DateTime.Now.AddDays(-1);

            var viewModel = new DailyEarningViewModel
            {
                ProductList = param.IsOnlyContent ? null : _statementHistoryRepository.GetProductList(param.IsInternal),
                Data = _statementHistoryRepository.GetDailyEarning(param),
                SelectedProduct = param.ProductId.Value
            };

            ViewBag.MonthDate = param.MonthDate;
            ViewBag.BaseCurrencies = _currencyRepository.GetBaseCurrencyList();
            ViewBag.SelectedBaseCurrency = param.BaseCurrency;

            var viewPath = param.IsOnlyContent 
                ? "~/Views/UserPanel/StatementHistory/StatementHistoryPartial/_DailyEarningContent.cshtml" 
                : "~/Views/UserPanel/StatementHistory/DailyEarning.cshtml";

            return View(viewPath, viewModel);

        }
    }
}