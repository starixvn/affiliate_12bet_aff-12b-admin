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
            return View("~/Views/TermAndCondition/Index.cshtml", model: _sessionService.CurrentLanguage);
        }

        [HttpGet]
        public ActionResult Popup()
        {
            return View("~/Views/TermAndCondition/Popup.cshtml", model: _sessionService.CurrentLanguage);
        }
    }
}