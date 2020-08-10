using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.AdminRole;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.ViewModels.AdminRole;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.AdminRole
{
    [FunctionAuthorize(FunctionName = Function.UserControl)]
    public class AdminInfoController : BaseController
    {
        private readonly IAdminInfoService _adminInfoService;

        public AdminInfoController(
            ISessionService sessionService,
            IAdminInfoService adminInfoService) : base(sessionService)
        {
            _adminInfoService = adminInfoService;
        }

        [HttpGet]
        public ActionResult Index(int userId = 0)
        {
            var userInfo = new AdminInfo();
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.AdminInfo, Url = Url.Content($"~/Portal/AdminInfo?userId={userId}"), IsActive = true }
            };
            var roleList = _adminInfoService.GetRoleList();

            if (userId >= Validation.MinId)
            {
                subMenu.Add(new SubMenu { Name = Lang.PermissionRole, Url = Url.Content($"~/Portal/PermissionRole?userId={userId}") });

                userInfo = _adminInfoService.GetUserInfo(userId);
            }

            var model = new AdminInfoViewModel
            {
                UserId = userId,
                UserName = userInfo?.UserName ?? string.Empty,
                SubMenuList = subMenu,
                UserInfo = userInfo,
                RoleList = roleList
            };

            return View("~/Areas/Portal/Views/AdminRole/AdminInfo.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetUserInfo(int userId)
        {
            return new JsonNetResult
            {
                Data = _adminInfoService.GetUserInfo(userId)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetRoleList()
        {
            return new JsonNetResult
            {
                Data = _adminInfoService.GetRoleList()
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult CreateUser(AdminInfoParam param)
        {
            if (ModelState.IsValid)
            {
                param.ByUserId = SessionService.UserId;
                param.Ip = Request.UserHostAddress;

                return new JsonNetResult
                {
                    Data = _adminInfoService.CreateUser(param)
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
        public ActionResult UpdateUser(AdminInfoParam param)
        {
            if (ModelState.IsValid && param.UserId >= Validation.MinId)
            {
                param.ByUserId = SessionService.UserId;
                param.Ip = Request.UserHostAddress;

                return new JsonNetResult
                {
                    Data = _adminInfoService.UpdateUser(param)
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
