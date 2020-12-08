using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Home;
using Fanex.Affiliate.Admin.Core.Utilities;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.HomePage)]
    public class HomeController : ReadOnlySessionBaseController
    {
        private readonly IHomeService _homeService;
        private readonly ILanguageService _languageService;

        public HomeController(
            ISessionService sessionService,
            IHomeService homeService,
            ILanguageService languageService) : base(sessionService)
        {
            _homeService = homeService;
            _languageService = languageService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
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
            var supportLanguages = _languageService.GetInChangeMarkets();
            return LanguageHelper.BuildAllowQueryLanguages(string.Empty, supportLanguages);

        }
    }
}