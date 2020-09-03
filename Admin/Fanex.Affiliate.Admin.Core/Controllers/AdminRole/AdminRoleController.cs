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
    public class AdminRoleController : BaseController
    {
        private readonly IAdminUsersService _adminUsersService;
        private readonly IAdminInfoService _adminInfoService;

        public AdminRoleController(
            ISessionService sessionService,
            IAdminUsersService adminUsersService,
            IAdminInfoService adminInfoService) : base(sessionService)
        {
            _adminUsersService = adminUsersService;
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
                Data = _adminUsersService.GetUserList(userName)
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
        public ActionResult DeleteUsers(DeleteAdminUsersParam param)
        {
            if (ModelState.IsValid)
            {
                param.ByUserId = SessionService.UserId;
                param.Ip = Request.UserHostAddress;

                return new JsonNetResult
                {
                    Data = _adminUsersService.DeleteUsers(param)
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
