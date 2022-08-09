using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.Filters;
using Fanex.Affiliate.User.Core.ViewModels.UserPanel;
using System.Web.Mvc;

namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    [CustomAuthorize]
    public class FeedbackController : BaseController
    {
        public FeedbackController(ISessionService sessionService,
            ILanguageService languageService) : base(sessionService, languageService)
        { }

        [HttpGet]
        [AjaxOnly]
        public ActionResult Index(bool isClickLogout = false)
        {
            var model = new FeedbackViewModel
            {
                UserName = SessionService.UserSession.UserName,
                IsClickLogout = isClickLogout
            };

            return View("~/Views/UserPanel/Feedback/Index.cshtml", model);
        }
    }
}
