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
        private readonly IAccountOverviewRepository _accountOverviewRepository;
        private readonly IMarketingService _marketingService;
        private readonly IStatisticsRepository _statisticsRepository;

        public UserPanelController(
            ISessionService sessionService,
            IAppSetting appSetting,
            IAccountRepository accountRepository,
            IAccountOverviewRepository accountOverviewRepository,
            IMarketingService marketingService,
            IStatisticsRepository statisticsRepository)
            : base(sessionService, appSetting)
        {
            _accountRepository = accountRepository;
            _accountOverviewRepository = accountOverviewRepository;
            _marketingService = marketingService;
            _statisticsRepository = statisticsRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }
    }
}