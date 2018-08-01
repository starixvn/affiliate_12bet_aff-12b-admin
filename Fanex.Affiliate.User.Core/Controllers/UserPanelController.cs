namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;
    using BusinessService;
    using CoreService;
    using Filters;
    using Repository.UserPanel;
    using Utilities;

    [CustomAuthorize]
    public partial class UserPanelController : BaseController
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMarketingService _marketingService;
        private readonly IStatisticsRepository _statisticsRepository;
        private readonly IStatementHistoryRepository _statementHistoryRepository;
        private readonly IPaymentService _paymentService;
        private readonly ICommissionReportService _commissionReportService;
           
        public UserPanelController(
            ISessionService sessionService,
            IAppSetting appSetting,
            IAccountRepository accountRepository,
            IMarketingService marketingService,
            IStatisticsRepository statisticsRepository,
            IStatementHistoryRepository statementHistoryRepository,
            IPaymentService paymentService,
            ICommissionReportService commissionReportService
            )
            : base(sessionService, appSetting)
        {
            _accountRepository = accountRepository;
            _marketingService = marketingService;
            _statisticsRepository = statisticsRepository;
            _statementHistoryRepository = statementHistoryRepository;
            _paymentService = paymentService;
            _commissionReportService = commissionReportService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}