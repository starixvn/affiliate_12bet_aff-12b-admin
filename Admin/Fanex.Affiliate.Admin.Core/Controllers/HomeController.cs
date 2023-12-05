using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Home;
using Fanex.Affiliate.Admin.Core.Utilities;
using System.Linq;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.HomeMenu)]
    public class HomeController : ReadOnlySessionBaseController
    {
        private readonly IHomeService _homeService;
        private readonly ISystemService _systemService;

        public HomeController(
            ISessionService sessionService,
            IHomeService homeService,
            ISystemService systemService) : base(sessionService)
        {
            _homeService = homeService;
            _systemService = systemService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowByPassFunctionAuthorize]
        public ActionResult RedirectMenu()
        {
            var permissions = SessionService.ClaimIdentities.SelectMany(i => i.Claims);
            var activeMenus = Menu.MainMenus.Where(m => string.IsNullOrEmpty(m.Value) || permissions.Any(p => p.Type == m.Value)).Select(m => m.Key);
            var redirect = activeMenus.FirstOrDefault();

            return Redirect($"~/Portal/{redirect}");
        }

        [HttpPost]
        public ActionResult GetPSPToday()
        {
            return new JsonNetResult
            {
                Data = _homeService.GetPSPToday()
            };
        }

        [HttpPost]
        public ActionResult GetDepositInfo()
        {
            return new JsonNetResult
            {
                Data = _homeService.GetDepositInfo()
            };
        }

        [HttpPost]
        public ActionResult GetGeneralInfo()
        {
            var marketIds = GetCurrentInChargeMarkets();
            return new JsonNetResult
            {
                Data = _homeService.GetGeneralInfo(marketIds)
            };
        }

        [HttpPost]
        public ActionResult GetOnlineList(int pageIndex = 1, int pageSize = 20)
        {

            var marketIds = GetCurrentInChargeMarkets();
            return new JsonNetResult
            {
                Data = _homeService.GetOnlineList(marketIds, pageIndex, pageSize)
            };
        }

        private string GetCurrentInChargeMarkets()
        {
            var supportLanguages = _systemService.GetInChangeMarkets();
            return SystemHelper.BuildAllowQueryLanguages(string.Empty, supportLanguages);

        }
    }
}