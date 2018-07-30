using System.Linq;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Core;

namespace Fanex.Affiliate.Admin.Core.Controllers
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

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordParam param)
        {
            if (ModelState.IsValid)
            {
                var updateParam = new LeftPanelUpdateInfoParam
                {
                    UserId = SessionService.CustId,
                    AffId = SessionService.SelectedAffId,
                    Ip = Request.UserHostAddress,
                    Password = param.Password
                };

                var result = _affService.ChangePassword(updateParam);

                return Json(result);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);

                return Json(new ResultInfo
                {
                    Code = -1,
                    Message = string.Join(",", errors)
                });
            }
        }

        [HttpPost]
        public ActionResult UpdateStatus(AffInfo param)
        {
            return Json(null);
        }
    }
}