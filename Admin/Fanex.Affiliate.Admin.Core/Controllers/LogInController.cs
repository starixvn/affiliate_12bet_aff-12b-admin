using ONEbook.UM.AspNetMVC.Filters;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [CheckUMFilter]
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var isAuthorized = HttpContext.Request.IsAuthenticated && HttpContext.User.Identity.IsAuthenticated;

            if(isAuthorized)
            {
                return Redirect("~/Portal/Home");
            }

            return View();
        }
    }
}
