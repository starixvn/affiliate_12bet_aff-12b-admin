namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;
    using Filters;

    public partial class UserPanelController
    {
        [HttpGet]
        [AjaxOnly]
        public ActionResult TrackingStatistics()
        {
            return View();
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult ReferrersStatistics()
        {
            return View();
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult LandingPageStatistics()
        {
            return View();
        }
    }
}