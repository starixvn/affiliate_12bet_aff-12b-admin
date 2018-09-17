using System.Web.Mvc;
using System.Web.Security;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.Utilities;
using ONEbook.UM.Services;

namespace Fanex.Affiliate.User.Core.Controllers
{
    public class NotifyController : BaseController
    {

        public NotifyController(
            ISessionService sessionService, ILanguageService languageService)
            : base(sessionService, languageService)
        {
        }

        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PageNotFound()
        {
            return View();
        }
       
    }
}
