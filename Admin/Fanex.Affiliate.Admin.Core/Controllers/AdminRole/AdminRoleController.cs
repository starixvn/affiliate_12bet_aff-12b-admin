using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.Administrator;
using Fanex.Affiliate.Admin.Core.Services.AdminRole;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.ViewModels.AdminRole;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.AdminRole
{
    [FunctionAuthorize(FunctionName = Function.UserControl)]
    public class AdminRoleController : BaseController
    {
        private readonly IAdminUsersService _adminUsersService;
        private readonly IAdministratorService _administratorService;

        public AdminRoleController(
            ISessionService sessionService,
            IAdministratorService administratorService,
            IAdminUsersService adminUsersService) : base(sessionService)
        {
            _adminUsersService = adminUsersService;
            _administratorService = administratorService;
        }

        [HttpGet]
        public ActionResult Index(bool isBack = false)
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.AdminUsers, Url = Url.Content("~/Portal/AdminRole"), IsActive = true }
            };

            var model = new AdminRoleViewModel
            {
                SubMenuList = subMenu,
                IsBack = isBack
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetUserList(AdminUsersParam param)
        {
            return new JsonNetResult
            {
                Data = _administratorService.GetUserList(0, param.UserName, string.Empty)
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
