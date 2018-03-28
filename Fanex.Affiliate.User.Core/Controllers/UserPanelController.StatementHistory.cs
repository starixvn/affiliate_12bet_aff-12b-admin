namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;

    public partial class UserPanelController : Controller
    {
        [HttpGet]
        public ActionResult StatementHistory()
        {
            return View();
        }
    }
}