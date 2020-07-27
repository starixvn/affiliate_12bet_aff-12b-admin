using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.AdminRole;
using Fanex.Affiliate.Admin.Core.Services.Common;
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
        public ActionResult Index()
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.AdminInfo, Url = Url.Content("~/Portal/AdminInfo") },
                new SubMenu { Name = Lang.PermissionRole, Url = Url.Content("~/Portal/PermissionRole"), IsActive = true }
            };

            return View("~/Areas/Portal/Views/AdminRole/PermissionRole.cshtml", subMenu);
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
