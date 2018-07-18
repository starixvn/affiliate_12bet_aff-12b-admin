using System.Web.Mvc;

namespace AffiliateAdmin.Areas.Portal.Controllers
{
    public class AffiliateController : Controller
    {
        [HttpGet]
        public ActionResult Information()
        {
            return View();
        }
    }
}
