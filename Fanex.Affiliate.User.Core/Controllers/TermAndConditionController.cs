using System.Web.Mvc;

namespace Fanex.Affiliate.User.Core.Controllers
{
    public class TermAndConditionController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
