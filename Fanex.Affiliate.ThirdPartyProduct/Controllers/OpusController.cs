using System.Web.Mvc;

namespace Fanex.Affiliate.ThirdPartyProduct.Controllers
{
    public class OpusController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/ThirdPartyProduct/Opus/Index.cshtml");
        }
    }
}
