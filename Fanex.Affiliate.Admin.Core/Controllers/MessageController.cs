using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Services.Common;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    public class MessageController : BaseController
    {
        public MessageController(ISessionService sessionService) : base(sessionService)
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
