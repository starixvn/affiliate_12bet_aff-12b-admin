using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class UserAccountController : BaseController
    {
        private readonly IUserAccountRepository _userAccountRepository;

        public UserAccountController(
            ISessionService sessionService,
            IUserAccountRepository userAccountRepository) : base(sessionService)
        {
            _userAccountRepository = userAccountRepository;
        }

        [HttpGet]
        public ActionResult Index(AffiliateLinkParam param)
        {
            param.AffId = param.AffId == 0 ? SessionService.AffId : param.AffId;

            return View("~/Areas/Portal/Views/UserPanel/UserAccount/Index.cshtml", param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public JsonResult GetUserAccountInfo (int affId)
        {
            return new JsonResult
            {
                Data = _userAccountRepository.GetUserAccountInfo(affId == 0 ? SessionService.AffId : affId)
            };
        }

        [FunctionAuthorize(FunctionName = Function.Affiliate, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult UpdateUserAccount(UserAccountParam param)
        {
            if(ModelState.IsValid)
            {
                param.AffId = param.AffId == 0 ? SessionService.AffId : param.AffId;
                param.UserId = SessionService.UserId;
                param.Ip = Request.UserHostAddress;

                return new JsonResult
                {
                    Data = _userAccountRepository.UpdateUserAccount(param)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "Update User Account is failed!"
                }
            };
        }
    }
}
