using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using Fanex.Affiliate.Admin.Core.Services.AdminRole;
using Fanex.Affiliate.Admin.Core.Services.Common;
using ONEbook.UM.AspNetMVC.Filters;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.AdminRole
{
    [CustomAuthorize]
    [CheckUMFilter]
    [FunctionAuthorize(FunctionName = Function.AdminRoleMenu)]
    public class RolePermissionController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly IRolePermissionService _rolePermissionService;

        public RolePermissionController(
            ISessionService sessionService,
            IRolePermissionService rolePermissionService)
        {
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
        [FunctionAuthorize(FunctionName = Function.AdminRoleMenu, IsHost = true)]
        public ActionResult UpdatePermissionInfo(UpdateRolePermissionParam param)
        {
            if (ModelState.IsValid)
            {
                AssignUserInfoParam(param);

                var result = _rolePermissionService.UpdatePermissionInfo(param);

                return new JsonNetResult
                {
                    Data = result
                };
            }

            return ModelState.GenerateErrorResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.AdminRoleMenu, IsHost = true)]
        public ActionResult UpdateMarketInfoList(UpdateMarketInfoParam param)
        {
            if (ModelState.IsValid)
            {
                AssignUserInfoParam(param);

                var result = _rolePermissionService.UpdateMarketInfoList(param);

                return new JsonNetResult
                {
                    Data = result
                };
            }

            return ModelState.GenerateErrorResult();
        }

        private void AssignUserInfoParam(UserInfoParam param)
        {
            param.UserId = _sessionService.UserId;
            param.Ip = Request.GetClientIP();
        }
    }
}
