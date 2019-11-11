using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Home;
using Fanex.Affiliate.Admin.Core.Extensions;

namespace Fanex.Affiliate.Admin.Core.Controllers

{
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
        public ActionResult GetOnlineList(int pageIndex = 1, int pageSize = 20)
        {
            return new JsonNetResult
            {
                Data = _homeService.GetOnlineList(pageIndex, pageSize)
            };
        }
    }
}