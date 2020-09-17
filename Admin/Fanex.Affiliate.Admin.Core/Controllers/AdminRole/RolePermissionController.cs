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
    public class RolePermissionController : BaseController
    {
        private readonly IRolePermissionService _rolePermissionService;

        public RolePermissionController(
            ISessionService sessionService,
            IRolePermissionService rolePermissionService) : base(sessionService)
        {
            _rolePermissionService = rolePermissionService;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetPermissionInfoList()
        {
            return new JsonNetResult
            {
                Data = _rolePermissionService.GetPermissionInfoList()
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult UpdatePermissionInfo(UpdateRolePermissionParam param)
        {
            if (ModelState.IsValid)
            {
                param.UserId = SessionService.UserId;
                param.Ip = Request.UserHostAddress;

                return new JsonNetResult
                {
                    Data = _rolePermissionService.UpdatePermissionInfo(param)
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
        public ActionResult UpdateMarketInfoList(UpdateMarketInfoParam param)
        {
            if (ModelState.IsValid)
            {
                param.UserId = SessionService.UserId;
                param.Ip = Request.UserHostAddress;

                return new JsonNetResult
                {
                    Data = _rolePermissionService.UpdateMarketInfoList(param)
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
