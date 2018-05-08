namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;

    public partial class UserPanelController
    {
        [HttpGet]
        public ActionResult PaymentRequest()
        {
            return View();
        }
    }
}