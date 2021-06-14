namespace Fanex.Affiliate.User.Core.Controllers
{
    using CoreService;
    using Fanex.Affiliate.User.Core.BusinessService;
    using System.Web.Mvc;

    public class PromotionController : BaseController
    {

        public PromotionController(ISessionService sessionService, ILanguageService languageService)
            : base(sessionService, languageService)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Views/Promotion/Index.cshtml", model: SessionService.CurrentLanguage);
        }
    }
}