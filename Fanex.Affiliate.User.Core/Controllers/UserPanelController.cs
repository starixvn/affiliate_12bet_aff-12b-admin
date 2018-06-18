namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;
    using BusinessService;
    using CoreService;
    using Filters;
    using Repository.UserPanel;
    using Utilities;

    [Authenticate]
    public partial class UserPanelController : BaseController
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMarketingService _marketingService;
        private readonly IStatisticsRepository _statisticsRepository;
        private readonly IStatementHistoryRepository _statementHistoryRepository;
        private readonly ICommissionRepository _commissionRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IEmailService _emailService;
        private readonly IAccountService _accountService;
        private readonly INewsService _newsService;
        private readonly IPaymentService _paymentService;

        public UserPanelController(
            ISessionService sessionService,
            IAppSetting appSetting,
            IAccountRepository accountRepository,
            IMarketingService marketingService,
            IStatisticsRepository statisticsRepository,
            IStatementHistoryRepository statementHistoryRepository,
            ICommissionRepository commissionRepository,
            IPaymentRepository paymentRepository,
            IEmailService emailService,
            IAccountService accountService,
            INewsService newsService,
            IPaymentService paymentService)
            : base(sessionService, appSetting)
        {
            _accountRepository = accountRepository;
            _marketingService = marketingService;
            _statisticsRepository = statisticsRepository;
            _statementHistoryRepository = statementHistoryRepository;
            _commissionRepository = commissionRepository;
            _paymentRepository = paymentRepository;
            _emailService = emailService;
            _accountService = accountService;
            _newsService = newsService;
            _paymentService = paymentService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}