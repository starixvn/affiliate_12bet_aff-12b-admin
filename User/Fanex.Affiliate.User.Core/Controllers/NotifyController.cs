using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;
using System.Web.Mvc;

namespace Fanex.Affiliate.User.Core.Controllers
{
    public class NotifyController : BaseController
    {

        public NotifyController(
            ISessionService sessionService, ISystemService systemsService)
            : base(sessionService, systemsService)
        {
        }

        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PageNotFound()
        {
            return View();
        }

        public ActionResult CodeExpired()
        {
            return Json(new
            {
                IsOk = false,
                IsExpired = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
