using System.Web.Mvc;
using ONEbook.UM.Filters;

namespace Fanex.Affiliate.User.Core.Controllers
{
    [CheckUMFilter]
    public class TermAndConditionController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Popup()
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
