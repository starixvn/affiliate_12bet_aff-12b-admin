using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.AdminRole;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.ViewModels.AdminRole;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.AdminRole
{
    [FunctionAuthorize(FunctionName = Function.UserControl)]
    public class PermissionRoleController : BaseController
    {
        private readonly IAdminInfoService _adminInfoService;
        private readonly IPermissionRoleService _permissionRoleService;
        public PermissionRoleController(
            ISessionService sessionService,
            IAdminInfoService adminInfoService,
            IPermissionRoleService permissionRoleService) : base(sessionService)
        {
            _adminInfoService = adminInfoService;
            _permissionRoleService = permissionRoleService;
        }

        [HttpGet]
        public ActionResult Index(int userId)
        {
            if (userId >= Validation.MinId)
            {
                var subMenu = new List<SubMenu>
                {
                    new SubMenu { Name = Lang.AdminInfo, Url = Url.Content($"~/Portal/AdminInfo?userId={userId}") },
                    new SubMenu { Name = Lang.PermissionRole, Url = Url.Content($"~/Portal/PermissionRole?userId={userId}"), IsActive = true }
                };

                var userInfo = _adminInfoService.GetUserInfo(userId);

                var model = new PermissionRoleViewModel
                {
                    UserId = userId,
                    SubMenuList = subMenu,
                    UserName = userInfo?.UserName ?? string.Empty,
                    PermissionRoleInfoList = new List<PermissionRoleInfo>()
                };

                return View("~/Areas/Portal/Views/AdminRole/PermissionRole.cshtml", model);
            }

            return Redirect("~/Portal/AdminInfo");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetFunctionList(int userId)
        {
            return new JsonNetResult
            {
                Data = _permissionRoleService.GetPermissionRoleList(userId)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetLanguageList(int userId)
        {
            return new JsonNetResult
            {
                Data = _permissionRoleService.GetPermissionRoleList(userId, false)
            };
        }
    }
}
