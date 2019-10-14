using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    public class LogInController : Controller
    {
        public LogInController(ISessionService sessionService)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
