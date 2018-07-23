using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.MetaModels;
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

        [HttpPost]
        public ActionResult UpdateInfo(LeftPanelUpdateInfoParam param)
        {
            param.UserId = SessionService.CustId;
            param.AffId = SessionService.SelectedAffId;
            param.Ip = Request.UserHostAddress;

            var result = _affService.UpdateInfo(param);

            return Json(result);
        }
    }
}
