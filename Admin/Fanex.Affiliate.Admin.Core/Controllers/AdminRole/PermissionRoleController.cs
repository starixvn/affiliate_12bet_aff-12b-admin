using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Services.AdminRole;
using Fanex.Affiliate.Admin.Core.Services.Common;
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
