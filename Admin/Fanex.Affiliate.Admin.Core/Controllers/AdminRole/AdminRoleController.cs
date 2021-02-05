using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using Fanex.Affiliate.Admin.Core.Services.AdminRole;
using Fanex.Affiliate.Admin.Core.Services.Common;
using ONEbook.UM.Filters;
using System.Linq;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.AdminRole
{
    [CustomAuthorize]
    [CheckUMFilter]
    [FunctionAuthorize(FunctionName = Function.AdminRoleMenu)]
    public class AdminRoleController : ReadOnlySessionBaseController
    {
        private readonly IAdminListService _adminListService;
        private readonly IAdminInfoService _adminInfoService;

        public AdminRoleController(
            ISessionService sessionService,
            IAdminListService adminListService,
            IAdminInfoService adminInfoService) : base(sessionService)
        {
            _adminListService = adminListService;
            _adminInfoService = adminInfoService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetUserList(string userName)
        {
            return new JsonNetResult
            {
                Data = _adminListService.GetUserList(userName)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetUserPermissionList(int userId)
        {
            return new JsonNetResult
            {
                Data = _adminInfoService.GetUserInfo(userId).UserPermissionList
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.AdminRoleMenu, IsHost = true)]
        public ActionResult DeleteUsers(DeleteAdminUsersParam param)
        {
            if (ModelState.IsValid)
            {
                param.ByUserId = SessionService.UserId;
                param.Ip = Request.UserHostAddress;

                return new JsonNetResult
                {
                    Data = _adminListService.DeleteUsers(param)
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
