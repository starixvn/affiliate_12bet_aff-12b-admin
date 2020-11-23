using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;
using ONEbook.UM.Services;

namespace Fanex.Affiliate.User.Core.Controllers
{
    public class UnderMaintenanceController : Controller
    {
        private readonly ISessionService _sessionService;
        protected readonly ILanguageService _languageService;
        private readonly IMaintenanceService _maintenanceService;
        public UnderMaintenanceController(ISessionService sessionService,
            ILanguageService languageService, IMaintenanceService maintenanceService)
        {
            _sessionService = sessionService;
            _languageService = languageService;
            _maintenanceService = maintenanceService;
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            var favoriteLanguage = _languageService.GetFavoriteLanguage();
            var sessionLang = _sessionService.CurrentLanguage;
            if (favoriteLanguage != sessionLang)
            {
                _sessionService.CurrentLanguage = favoriteLanguage;
            }
            _languageService.SetCulture(favoriteLanguage);
        }

        [HttpGet]
        public ActionResult Index()
        {
            var maintenanceInfo = _maintenanceService.GetUnderMaintenanceTime();

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
