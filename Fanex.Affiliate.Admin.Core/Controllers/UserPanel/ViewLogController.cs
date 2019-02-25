using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    public class ViewLogController : BaseController
    {
        public ViewLogController(ISessionService sessionService) : base(sessionService)
        {
        }

        [HttpGet]
        public ActionResult Index(AffiliateLinkParam affiliateLinkParam)
        {
            return View("~/Areas/Portal/Views/UserPanel/ViewLog/Index.cshtml", affiliateLinkParam);
        }
    }
}
