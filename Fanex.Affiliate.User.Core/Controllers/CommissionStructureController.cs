namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;
    using CoreService;
    using Fanex.Affiliate.User.Core.BusinessService;

    public class CommissionStructureController : BaseController
    {

        public CommissionStructureController(ISessionService sessionService,
            ILanguageService languageService) : base(sessionService, languageService)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Views/CommissionStructure/Index.cshtml", model: SessionService.CurrentLanguage);
        }
    }
}