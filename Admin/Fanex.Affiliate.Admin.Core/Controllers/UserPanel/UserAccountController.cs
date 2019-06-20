using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;
using Fanex.Affiliate.Admin.Core.Resources;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class UserAccountController : BaseController
    {
        private readonly IUserAccountService _userAccountService;

        public UserAccountController(
            ISessionService sessionService,
            IUserAccountService userAccountService) : base(sessionService)
        {
            _userAccountService = userAccountService;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetUserAccountInfo (int affId)
        {
            if (affId >= Validation.MinId)
            {
                return new JsonNetResult
                {
                    Data = _userAccountService.GetUserAccountInfo(affId)
                };
            }

            return new JsonNetResult();
        }

        [FunctionAuthorize(FunctionName = Function.Affiliate, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult UpdateUserAccount(UserAccountParam param)
        {
            if(ModelState.IsValid)
            {
                param.UserId = SessionService.UserId;
                param.Ip = Request.UserHostAddress;

                return new JsonNetResult
                {
                    Data = _userAccountService.UpdateUserAccount(param)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = Lang.UpdateIsFailed
                }
            };
        }
    }
}
