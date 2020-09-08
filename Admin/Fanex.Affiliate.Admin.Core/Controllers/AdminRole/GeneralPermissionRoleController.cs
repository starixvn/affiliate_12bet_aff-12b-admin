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
        public ActionResult GetMarketByUserList()
        {
            const int userId = 1;
            return new JsonNetResult
            {
                Data = _adminRoleCommonService.GetMarketByUserList(userId)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetMarketByRoleList()
        {
            return new JsonNetResult
            {
                Data = _generalPermissionRoleService.GetMarketByRoleList()
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult UpdatePermissionInfo(UpdatePermissionInfoParam param)
        {
            if (ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = _generalPermissionRoleService.UpdatePermissionInfo(param)
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
                return new JsonNetResult
                {
                    Data = _generalPermissionRoleService.UpdateMarketInfoList(param)
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
