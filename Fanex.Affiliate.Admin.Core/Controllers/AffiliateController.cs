using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Core;

namespace AffiliateAdmin.Areas.Portal.Controllers
{
    public class AffiliateController : BaseController
    {
        private readonly IAffiliateService _affService;

        public AffiliateController(ISessionService sessionService, IAffiliateService affService) : base(sessionService)
        {
            _affService = affService;
        }

        [HttpGet]
        public ActionResult Information()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetInfo(string userName)
        {
            return Json(_affService.GetInfo(userName), JsonRequestBehavior.AllowGet);
        }
    }
}
