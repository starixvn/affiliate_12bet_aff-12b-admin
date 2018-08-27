namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;
    using CoreService;
    using ONEbook.UM.Filters;

    [CheckUMFilter]
    public class FaqController : Controller
    {
        private readonly ISessionService _sessionService;

        public FaqController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Views/Faq/Index.cshtml", model: _sessionService.CurrentLanguage);
        }
    }
}