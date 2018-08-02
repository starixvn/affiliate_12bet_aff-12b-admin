namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;
    using Filters;
    using ONEbook.UM.Filters;

    [CheckUMFilter]
    [CustomAuthorize]
    public class MainController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {

            return View("~/Views/UserPanel/Main/index.cshtml");
        }
    }
}