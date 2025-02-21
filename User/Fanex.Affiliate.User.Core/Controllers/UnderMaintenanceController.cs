using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Fanex.Affiliate.User.Core.Controllers
{
    public class UnderMaintenanceController : Controller
    {
        private readonly ISessionService _sessionService;
        protected readonly ISystemService _systemsService;
        private readonly IUnderMaintenanceService _underMaintenanceService;
        public UnderMaintenanceController(ISessionService sessionService,
            ISystemService systemsService, IUnderMaintenanceService underMaintenanceService)
        {
            _sessionService = sessionService;
            _systemsService = systemsService;
            _underMaintenanceService = underMaintenanceService;
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            var favoriteLanguage = _systemsService.GetFavoriteLanguage();
            var sessionLang = _sessionService.CurrentLanguage;
            if (favoriteLanguage != sessionLang)
            {
                _sessionService.CurrentLanguage = favoriteLanguage;
            }
            _systemsService.SetCulture(favoriteLanguage);
        }

        [HttpGet]
        public ActionResult Index()
        {
            var maintenanceInfo = _underMaintenanceService.GetUnderMaintenanceTime();

            if (!maintenanceInfo.IsUnderMaintenanceTime)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            ViewBag.UmTimeFrom = maintenanceInfo.From;
            ViewBag.UmTimeTo = maintenanceInfo.To;

            return View();
        }
    }
}
