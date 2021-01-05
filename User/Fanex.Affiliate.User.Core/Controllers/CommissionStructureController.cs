namespace Fanex.Affiliate.User.Core.Controllers
{
    using CoreService;
    using Fanex.Affiliate.User.Core.BusinessService;
    using System.Web.Mvc;

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