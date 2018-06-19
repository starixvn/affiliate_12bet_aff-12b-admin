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

        [HttpGet]
        [ChildActionOnly]
        [OutputCache(Duration = 86400, VaryByParam = "none")]
        public ActionResult Content()
        {
            return PartialView("~/Views/TermAndCondition/_Content.cshtml");
        }
    }
}
