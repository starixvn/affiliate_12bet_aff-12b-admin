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
            return View("~/Views/CommissionStructure/Index.cshtml", model: _sessionService.CurrentLanguage);
        }
    }
}