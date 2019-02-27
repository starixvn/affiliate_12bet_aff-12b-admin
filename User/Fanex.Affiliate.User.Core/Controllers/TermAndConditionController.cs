using System.Web.Mvc;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;

namespace Fanex.Affiliate.User.Core.Controllers
{
    public class TermAndConditionController : BaseController
    {

        public TermAndConditionController(ISessionService sessionService, ILanguageService languageService) :
            base(sessionService, languageService)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Views/TermAndCondition/Index.cshtml", model: SessionService.CurrentLanguage);
        }

        [HttpGet]
        public ActionResult Popup()
        {
            return View("~/Views/TermAndCondition/Popup.cshtml", model: SessionService.CurrentLanguage);
        }
    }
}