using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    public class UserAccountController : BaseController
    {
        public UserAccountController(ISessionService sessionService) : base(sessionService)
        {
        }

        [HttpGet]
        public ActionResult Index(AffiliateLinkParam param)
        {
            param.AffId = param.AffId == 0 ? SessionService.AffId : param.AffId;

            return View("~/Areas/Portal/Views/UserPanel/UserAccount/Index.cshtml", param);
        }
    }
}
