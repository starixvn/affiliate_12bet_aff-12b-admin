using System.Web.Mvc;
using Fanex.Affiliate.ThirdPartyProduct.Attributes;

namespace Fanex.Affiliate.ThirdPartyProduct.Controllers
{
    [Authorize]
    [FunctionAuthorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/ThirdPartyProduct/Home/Index.cshtml");
        }
    }
}
