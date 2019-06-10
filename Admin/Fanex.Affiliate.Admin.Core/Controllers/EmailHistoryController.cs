using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    public class EmailHistoryController : BaseController
    {
        public EmailHistoryController(ISessionService sessionService) : base(sessionService)
        {
        }

        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/EmailHistory/Index.cshtml");
        }
    }
}
