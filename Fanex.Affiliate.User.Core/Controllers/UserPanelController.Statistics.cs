namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;

    public partial class UserPanelController : Controller
    {
        [HttpGet]
        public ActionResult TrackingStatistics()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ReferrersStatistics()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LandingPageStatistics()
        {
            return View();
        }
    }
}