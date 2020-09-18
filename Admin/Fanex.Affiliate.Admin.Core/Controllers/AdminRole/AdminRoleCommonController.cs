using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Services.AdminRole;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.AdminRole
{
    [FunctionAuthorize(FunctionName = Function.UserControl)]
    public class AdminRoleCommonController : BaseController
    {
        private readonly IAdminRoleCommonService _adminRoleCommonService;

        public AdminRoleCommonController(
            ISessionService sessionService,
            IAdminRoleCommonService adminRoleCommonService) : base(sessionService)
        {
            _adminRoleCommonService = adminRoleCommonService;
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
        public ActionResult GetMarketListByUser(int? userId = null)
        {
            return new JsonNetResult
            {
                Data = _adminRoleCommonService.GetMarketListByUser(userId)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetMarketListByRole(int? roleId = null)
        {
            return new JsonNetResult
            {
                Data = _adminRoleCommonService.GetMarketListByRole(roleId)
            };
        }
    }
}