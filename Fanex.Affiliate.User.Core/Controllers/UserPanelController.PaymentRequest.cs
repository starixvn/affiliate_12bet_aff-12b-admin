namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;
    using Filters;

    public partial class UserPanelController
    {
        [HttpGet]
        [AjaxOnly]
        public ActionResult PaymentRequest()
        {
            return View();
        }
    }
}