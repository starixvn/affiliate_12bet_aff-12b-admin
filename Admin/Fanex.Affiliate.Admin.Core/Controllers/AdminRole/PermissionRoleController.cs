using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.AdminRole
{
    [FunctionAuthorize(FunctionName = Function.UserControl)]
    public class PermissionRoleController : BaseController
    {
        public PermissionRoleController(ISessionService sessionService) : base(sessionService) { }

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
    }
}
