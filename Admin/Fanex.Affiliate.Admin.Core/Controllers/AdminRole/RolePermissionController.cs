using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.AdminRole;
using Fanex.Affiliate.Admin.Core.Services.Common;
using ONEbook.UM.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.AdminRole
{
    [CustomAuthorize]
    [CheckUMFilter]
    [FunctionAuthorize(FunctionName = Function.UserControl)]
    public class RolePermissionController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ISessionService _sessionService;
        private readonly IRolePermissionService _rolePermissionService;

        public RolePermissionController(
            IAuthService authService,
            ISessionService sessionService,
            IRolePermissionService rolePermissionService)
        {
            _authService = authService;
            _sessionService = sessionService;
            _rolePermissionService = rolePermissionService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/AdminRole/RolePermission.cshtml");
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
        [FunctionAuthorize(FunctionName = Function.UserControl, IsHost = true)]
        public ActionResult UpdatePermissionInfo(UpdateRolePermissionParam param)
        {
            if (ModelState.IsValid)
            {
                param.UserId = _sessionService.UserId;
                param.Ip = Request.UserHostAddress;

                var result = _rolePermissionService.UpdatePermissionInfo(param);
                UpdateClaims(result.IsSuccessful);

                return new JsonNetResult
                {
                    Data = result
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
        [FunctionAuthorize(FunctionName = Function.UserControl, IsHost = true)]
        public ActionResult UpdateMarketInfoList(UpdateMarketInfoParam param)
        {
            if (ModelState.IsValid)
            {
                param.UserId = _sessionService.UserId;
                param.Ip = Request.UserHostAddress;

                var result = _rolePermissionService.UpdateMarketInfoList(param);
                UpdateClaims(result.IsSuccessful);

                return new JsonNetResult
                {
                    Data = result
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

        private void UpdateClaims(bool isSuccessful)
        {
            if (isSuccessful)
            {
                var claims = _authService.GetClaims(_sessionService.UserId);
                _sessionService.ClaimIdentities = new List<ClaimsIdentity> { new ClaimsIdentity(claims) };
            }
        }
    }
}
