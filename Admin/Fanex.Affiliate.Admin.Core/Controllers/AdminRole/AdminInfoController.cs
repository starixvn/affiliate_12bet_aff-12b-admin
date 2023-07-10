using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.AdminRole;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.AdminRole;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Utilities;
using ONEbook.UM.AspNetMVC.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.AdminRole
{
    [CustomAuthorize]
    [CheckUMFilter]
    [FunctionAuthorize(FunctionName = Function.AdminRoleMenu)]
    public class AdminInfoController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ISessionService _sessionService;
        private readonly IAdminInfoService _adminInfoService;

        public AdminInfoController(
            IAuthService authService,
            ISessionService sessionService,
            IAdminInfoService adminInfoService)
        {
            _authService = authService;
            _sessionService = sessionService;
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
        [FunctionAuthorize(FunctionName = Function.AdminRoleMenu, IsHost = true)]
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
                    IsClosed = param.IsClosed,
                    IsRoleMarketDefault = param.IsRoleMarketDefault,
                    IsRoleFunctionDefault = param.IsRoleFunctionDefault
                };

                GetUserInfoParam(encodeParam);

                return new JsonNetResult
                {
                    Data = _adminInfoService.CreateUser(encodeParam)
                };
            }

            return new JsonNetResult
            {
                Data = ResultInfo.GenerateErr(string.Join("<br />", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)))
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.AdminRoleMenu, IsHost = true)]
        public ActionResult UpdateUser(AdminInfoParam param)
        {
            if (ModelState.IsValid && param.UserId >= Validation.MinId)
            {
                var encodeParam = new AdminInfoParam
                {
                    UserId = param.UserId,
                    UserName = Server.HtmlEncode(param.UserName.Trim()),
                    FirstName = Server.HtmlEncode(param.FirstName.Trim()),
                    LastName = Server.HtmlEncode(param.LastName.Trim()),
                    Role = param.Role,
                    PermissionInfo = param.PermissionInfo,
                    MarketList = param.MarketList,
                    IsRoleMarketDefault = param.IsRoleMarketDefault,
                    IsRoleFunctionDefault = param.IsRoleFunctionDefault
                };

                GetUserInfoParam(encodeParam);

                var result = _adminInfoService.UpdateUser(encodeParam);

                UpdateClaims(result.IsSuccessful, encodeParam);

                return new JsonNetResult
                {
                    Data = result
                };
            }

            return new JsonNetResult
            {
                Data = ResultInfo.GenerateErr(string.Join("<br />", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)))
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.AdminRoleMenu, IsHost = true)]
        public ActionResult UpdateStatus(AdminInfoParam param)
        {
            if (param.UserId >= Validation.MinId)
            {
                GetUserInfoParam(param);

                var result = _adminInfoService.UpdateStatus(param);

                UpdateClaims(result.IsSuccessful, param);

                return new JsonNetResult
                {
                    Data = result
                };
            }

            return new JsonNetResult
            {
                Data = ResultInfo.GenerateErr("User is invalid!")
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.AdminRoleMenu, IsHost = true)]
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
                Data = ResultInfo.GenerateErr(string.Join("<br />", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)))
            };
        }

        private void GetUserInfoParam(AdminInfoParam param)
        {
            param.ByUserId = _sessionService.UserId;
            param.Ip = Request.UserHostAddress;
        }

        private void UpdateClaims(bool isSuccessful, AdminInfoParam param)
        {
            if (isSuccessful && param.UserId == param.ByUserId)
            {
                var claims = _authService.GetClaims(param.UserId);
                _sessionService.ClaimIdentities = new List<ClaimsIdentity> { new ClaimsIdentity(claims) };
            }
        }
    }
}
