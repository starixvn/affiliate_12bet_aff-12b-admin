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
        private readonly IPermissionRoleService _permissionRoleService;
        public PermissionRoleController(
            ISessionService sessionService,
            IPermissionRoleService permissionRoleService) : base(sessionService)
        {
            _permissionRoleService = permissionRoleService;
        }

        [HttpGet]
        public ActionResult Index(string userName)
        {
            var userNameAfterTrim = userName?.Trim();
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.AdminInfo, Url = Url.Content($"~/Portal/AdminInfo?userName={userNameAfterTrim}") },
                new SubMenu { Name = Lang.PermissionRole, Url = Url.Content($"~/Portal/PermissionRole?userName={userNameAfterTrim}"), IsActive = true }
            };

            if (string.IsNullOrEmpty(userNameAfterTrim)) //Or User Name is invalid
            {
                return Redirect("~/Portal/AdminInfo");
            }

            var model = new PermissionRoleViewModel
            {
                SubMenuList = subMenu,
                UserName = userNameAfterTrim,
                PermissionRoleInfoList = new List<PermissionRoleInfo>()
            };

            return View("~/Areas/Portal/Views/AdminRole/PermissionRole.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetFunctionList(string userName)
        {
            return new JsonNetResult
            {
                Data = _permissionRoleService.GetPermissionRoleList(userName)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetLanguageList(string userName)
        {
            return new JsonNetResult
            {
                Data = _permissionRoleService.GetPermissionRoleList(userName, false)
            };
        }
    }
}
