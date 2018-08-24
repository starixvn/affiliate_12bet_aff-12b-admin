namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;
    using CoreService;
    using ONEbook.UM.Filters;

    [CheckUMFilter]
    public class CommissionStructureController : Controller
    {
        private readonly ISessionService _sessionService;

        public CommissionStructureController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ChildActionOnly]
        [OutputCache(Duration = 86400, VaryByParam = "none", VaryByCustom = "language")]
        public ActionResult Content()
        {
            var viewName = $"~/Views/CommissionStructure/_Content.{_sessionService.CurrentLanguage}.cshtml";

            return PartialView(viewName);
        }
    }
}