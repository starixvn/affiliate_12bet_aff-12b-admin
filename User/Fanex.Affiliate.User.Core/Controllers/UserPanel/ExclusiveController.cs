using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    [CustomAuthorize]
    public class ExclusiveController : BaseController
    {
        private readonly IExclusiveService _exclusiveService;

        public ExclusiveController(
            IExclusiveService exclusiveService,
            ISessionService sessionService,
            ILanguageService languageService)
            : base(sessionService, languageService)
        {
            _exclusiveService = exclusiveService;
        }

        public async Task<ActionResult> Index()
        {
            //ViewBag.Content = await await _exclusiveService.GetExclusiveById(SessionService.UserSession.ExclusiveId);
            return View("~/Views/UserPanel/Exclusive/Index.cshtml");
        }

        [AllowAnonymous]
        public async Task<ActionResult> Preview(int id)
        {
            ViewBag.Content = await _exclusiveService.GetExclusiveById(id);
            return View("~/Views/UserPanel/Exclusive/Preview.cshtml");
        }
    }
}
