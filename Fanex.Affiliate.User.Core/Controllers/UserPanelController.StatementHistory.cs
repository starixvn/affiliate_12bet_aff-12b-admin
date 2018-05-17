namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;
    using Filters;

    public partial class UserPanelController
    {
        [HttpGet]
        [AjaxOnly]
        public ActionResult StatementHistory()
        {
            return View();
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult DailyEarning()
        {
            return View();
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult DailyEarningDetail()
        {
            return View();
        }
    }
}