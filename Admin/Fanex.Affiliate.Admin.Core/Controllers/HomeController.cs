using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Home;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class HomeController : BaseController
    {
        private readonly IHomeService _homeService;

        public HomeController(
            ISessionService sessionService,
            IHomeService homeService) : base(sessionService)
        {
            _homeService = homeService;
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
            return new JsonNetResult
            {
                Data = _homeService.GetGeneralInfo()
            };
        }

        [HttpPost]
        public ActionResult GetOnlineList(int pageIndex = 1, int pageSize = 20)
        {
            return new JsonNetResult
            {
                Data = _homeService.GetOnlineList(pageIndex, pageSize)
            };
        }
    }
}