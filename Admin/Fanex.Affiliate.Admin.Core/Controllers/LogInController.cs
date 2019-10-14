using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
