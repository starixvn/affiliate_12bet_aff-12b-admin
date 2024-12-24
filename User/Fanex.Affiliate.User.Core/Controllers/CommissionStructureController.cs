namespace Fanex.Affiliate.User.Core.Controllers
{
    using CoreService;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.Filters;
    using System.Web.Mvc;

    public class CommissionStructureController : BaseController
    {

        public CommissionStructureController(ISessionService sessionService,
            ISystemService systemsService) : base(sessionService, systemsService)
        {
        }

        [HttpGet]
        [LanguageRouteChecking]
        public ActionResult Index()
        {
            return View("~/Views/CommissionStructure/Index.cshtml", model: SessionService.CurrentLanguage);
        }
    }
}