using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using Fanex.Affiliate.Admin.Core.Services.AdminRole;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Linq;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.AdminRole
{
    [FunctionAuthorize(FunctionName = Function.UserControl)]
    public class AdminInfoController : BaseController
    {
        private readonly IAdminInfoService _adminInfoService;
        private readonly IAdminRoleCommonService _adminRoleCommonService;

        public AdminInfoController(
            ISessionService sessionService,
            IAdminInfoService adminInfoService,
            IAdminRoleCommonService adminRoleCommonService) : base(sessionService)
        {
            _adminInfoService = adminInfoService;
            _adminRoleCommonService = adminRoleCommonService;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetUserInfo(int userId)
        {
            return new JsonNetResult
            {
                Data = _adminInfoService.GetUserInfo(userId)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetRoleList()
        {
            return new JsonNetResult
            {
                Data = _adminRoleCommonService.GetRoleList()
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult CreateUser(AdminInfoParam param)
        {
            if (ModelState.IsValid)
            {
                param.ByUserId = SessionService.UserId;
                param.Ip = Request.UserHostAddress;

                return new JsonNetResult
                {
                    Data = _adminInfoService.CreateUser(param)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = string.Join("<br />", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult UpdateUser(AdminInfoParam param)
        {
            if (ModelState.IsValid && param.UserId >= Validation.MinId)
            {
                param.ByUserId = SessionService.UserId;
                param.Ip = Request.UserHostAddress;

                return new JsonNetResult
                {
                    Data = _adminInfoService.UpdateUser(param)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = string.Join("<br />", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                }
            };
        }
    }
}
