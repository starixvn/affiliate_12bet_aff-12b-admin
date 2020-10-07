using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Administrator;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using Fanex.Affiliate.Admin.Core.Services.AdminRole;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Utilities;
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
        public ActionResult GetPermissionListByRole(int roleId)
        {
            return new JsonNetResult
            {
                Data = _adminInfoService.GetPermissionListByRole(roleId)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult CreateUser(AdminInfoParam param)
        {
            if (ModelState.IsValid)
            {
                var encodeParam = new AdminInfoParam
                {
                    UserName = Server.HtmlEncode(param.UserName.Trim()),
                    Password = CryptographyHelper.Encrypt(param.Password.Trim()),
                    FirstName = Server.HtmlEncode(param.FirstName.Trim()),
                    LastName = Server.HtmlEncode(param.LastName.Trim()),
                    Role = param.Role,
                    MarketList = param.MarketList,
                    PermissionInfo = param.PermissionInfo,
                    IsClosed = param.IsClosed
                };

                GetUserInfoParam(encodeParam);

                return new JsonNetResult
                {
                    Data = _adminInfoService.CreateUser(encodeParam)
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
                var encodeParam = new AdminInfoParam
                {
                    UserName = Server.HtmlEncode(param.UserName.Trim()),
                    FirstName = Server.HtmlEncode(param.FirstName.Trim()),
                    LastName = Server.HtmlEncode(param.LastName.Trim()),
                    Role = param.Role,
                    PermissionInfo = param.PermissionInfo,
                    MarketList = param.MarketList
                };

                GetUserInfoParam(encodeParam);

                return new JsonNetResult
                {
                    Data = _adminInfoService.UpdateUser(encodeParam)
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
        public ActionResult UpdateStatus(AdminInfoParam param)
        {
            if (param.UserId >= Validation.MinId)
            {
                GetUserInfoParam(param);

                return new JsonNetResult
                {
                    Data = _adminInfoService.UpdateStatus(param)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "User is invalid!"
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult ChangePassword(UpdatePasswordParam param)
        {
            if (ModelState.IsValid)
            {
                var adminInfoParam = new AdminInfoParam
                {
                    UserId = param.AccountId,
                    Password = CryptographyHelper.Encrypt(param.Password)
                };

                GetUserInfoParam(adminInfoParam);

                return new JsonNetResult
                {
                    Data = _adminInfoService.ChangePassword(adminInfoParam)
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

        private void GetUserInfoParam(AdminInfoParam param)
        {
            param.ByUserId = SessionService.UserId;
            param.Ip = Request.UserHostAddress;
        }
    }
}
