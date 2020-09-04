using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Services.AdminRole;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.AdminRole
{
    [FunctionAuthorize(FunctionName = Function.UserControl)]
    public class GeneralPermissionRoleController : BaseController
    {
        private readonly IAdminRoleCommonService _adminRoleCommonService;
        private readonly IGeneralPermissionRoleService _generalPermissionRoleService;

        public GeneralPermissionRoleController(
            ISessionService sessionService,
            IAdminRoleCommonService adminRoleCommonService,
            IGeneralPermissionRoleService generalPermissionRoleService) : base(sessionService)
        {
            _adminRoleCommonService = adminRoleCommonService;
            _generalPermissionRoleService = generalPermissionRoleService;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetPermissionInfoList()
        {
            return new JsonNetResult
            {
                Data = _generalPermissionRoleService.GetPermissionInfoList()
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
        public ActionResult GetMarketList()
        {
            return new JsonNetResult
            {
                Data = _adminRoleCommonService.GetMarketList(SessionService.UserId)
            };
        }
    }
}
