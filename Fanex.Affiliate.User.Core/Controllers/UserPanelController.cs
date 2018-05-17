namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;
    using CoreService;
    using Filters;
    using Repository.UserPanel;
    using Utilities;

    [Authenticate]
    public partial class UserPanelController : BaseController
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountOverviewRepository _accountOverviewRepository;

        public UserPanelController(
            ISessionService sessionService,
            IAppSetting appSetting,
            IAccountRepository accountRepository,
            IAccountOverviewRepository accountOverviewRepository)
            : base(sessionService, appSetting)
        {
            _accountRepository = accountRepository;
            _accountOverviewRepository = accountOverviewRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }
    }
}