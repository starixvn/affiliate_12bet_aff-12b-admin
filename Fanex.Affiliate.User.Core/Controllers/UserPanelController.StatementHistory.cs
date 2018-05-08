namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;

    public partial class UserPanelController
    {
        [HttpGet]
        public ActionResult StatementHistory()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DailyEarning()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DailyEarningDetail()
        {
            return View();
        }
    }
}