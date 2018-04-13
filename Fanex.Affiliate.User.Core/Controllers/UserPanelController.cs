namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;

    [Authorize]
    public partial class UserPanelController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}