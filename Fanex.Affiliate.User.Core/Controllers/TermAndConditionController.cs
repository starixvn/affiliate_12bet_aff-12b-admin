using System.Web.Mvc;
using Fanex.Affiliate.User.Core.CoreService;
using ONEbook.UM.Filters;

namespace Fanex.Affiliate.User.Core.Controllers
{
    [CheckUMFilter]
    public class TermAndConditionController : Controller
    {
        private readonly ISessionService _sessionService;

        public TermAndConditionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Popup()
        {
            return View();
        }

        [HttpGet]
        [ChildActionOnly]
        [OutputCache(Duration = 86400, VaryByParam = "none", VaryByCustom = "language")]
        public ActionResult Content()
        {
            var viewName = $"~/Views/TermAndCondition/_Content.{_sessionService.CurrentLanguage}.cshtml";

            return PartialView(viewName);
        }
    }
}