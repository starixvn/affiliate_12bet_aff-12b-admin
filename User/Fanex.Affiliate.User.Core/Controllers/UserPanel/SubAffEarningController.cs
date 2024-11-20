namespace Fanex.Affiliate.User.Core.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using BusinessService;
    using CoreService;
    using Fanex.Affiliate.User.Core.Filters;
    using Fanex.Affiliate.User.Core.Repository.UserPanel;
    using MetalModels.UserPanel;
    using ViewModels.UserPanel;

    [CustomAuthorize]
    public class SubAffEarningController : BaseController
    {
        private readonly IStatementHistoryRepository _statementHistoryRepository;
        private readonly ICurrencyRepository _currencyRepository;

        public SubAffEarningController(
            ISessionService sessionService,
            ISystemService systemService,
            IStatementHistoryRepository statementHistoryRepository,
            ICurrencyRepository currencyRepository) : base(sessionService, systemService)
        {
            _statementHistoryRepository = statementHistoryRepository;
            _currencyRepository = currencyRepository;
        }

        [AjaxOnly]
        public ActionResult Index(SubAffComParam param)
        {
            param.AffId = SessionService.UserSession.AffId;
            param.IsInternal = SessionService.IsInternal;
            param.MonthDate = param.MonthDate ?? DateTimeOffset.Now.AddDays(-1).DateTime;

            var subAffComm = _statementHistoryRepository.GetSubAffCommission(param);
            var requestPage = param.RequestPage;
            var vms = subAffComm.Select(s => new SubAffComViewModel
            {
                Commission = s.Commission,
                SubAffCommission = s.SubAffCommission,
                SubAffUserName = s.UserName
            });

            if (!Constants.RequestPage.Links.TryGetValue(requestPage, out var breadcrumb))
            {
                breadcrumb = Constants.RequestPage.Links[Constants.RequestPage.StatementHistory];
            }

            breadcrumb.Url = $"{breadcrumb.Url}?baseCurrency={param.BaseCurrency}";

            ViewBag.MonthDate = param.MonthDate;
            ViewBag.Breadcrumb = breadcrumb;
            ViewBag.BaseCurrencies = _currencyRepository.GetBaseCurrencyList();
            ViewBag.SelectedBaseCurrency = param.BaseCurrency;

            var path = param.IsOnlyContent
                ? "~/Views/UserPanel/StatementHistory/StatementHistoryPartial/_SubAffEarningContent.cshtml"
                : "~/Views/UserPanel/StatementHistory/SubAffEarning.cshtml";

            return View(path, vms);
        }
    }
}
