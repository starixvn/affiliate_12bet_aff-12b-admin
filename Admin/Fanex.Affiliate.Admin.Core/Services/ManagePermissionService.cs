using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Fanex.Affiliate.Admin.Core.Services
{
    public class ManagePermissionService
    {
        protected int UserId { get; }

        protected IAuthService AuthService { get; }
        protected ISessionService SessionService { get; }
        protected IAffiliateService AffiliateService { get; }
        protected IEnumerable<Claim> UserClaims { get; }

        private bool IsPending { get; set; }

        public ManagePermissionService(
            IAuthService authService,
            ISessionService sessionService,
            IAffiliateService affiliateService)
        {
            UserId = sessionService.UserId;
            AuthService = authService;
            SessionService = sessionService;
            AffiliateService = affiliateService;
            UserClaims = authService.GetClaims(SessionService.UserId);
        }

        public bool HasReadFunctionPermission(string function)
        {
            return UserClaims.Any(p => p.Type.MatchWith(function));
        }

        public bool HasReadManagePendingAffPermission(int affId)
        {
            CheckPendingAff(affId);

            return HasReadFunctionPermission(Function.ManagePendingAff) && IsPending;
        }

        public bool HasReadFunctionOrManagePendingAffPermission(int affId, string function)
        {
            return HasReadManagePendingAffPermission(affId) || HasReadFunctionPermission(function);
        }

        public bool HasHostFunctionPermission(string function)
        {
            return UserClaims.Any(p => p.Type.MatchWith(function) && p.Value.MatchWith(Function.WritePermission));
        }

        public bool HasHostManagePendingAffPermission(int affId, string status = null)
        {
            CheckPendingAff(affId, status);

            return HasHostFunctionPermission(Function.ManagePendingAff) && IsPending;
        }

        public bool HasHostFunctionOrManagePendingAffPermission(int affId, string function)
        {
            var a = HasHostManagePendingAffPermission(affId);
            var b = HasHostFunctionPermission(function);
            return HasHostManagePendingAffPermission(affId) || HasHostFunctionPermission(function);
        }

        public bool HasReadMultiPermissions(int affId, string function)
        {
            return HasReadFunctionOrManagePendingAffPermission(affId, function)
                    && AuthService.IsUserHasPermissionOnAff(UserId, affId);
        }

        public bool HasHostMultiPermissions(int affId, string function)
        {
            var c = AuthService.IsUserHasPermissionOnAff(UserId, affId);
            return HasHostFunctionOrManagePendingAffPermission(affId, function)
                    && AuthService.IsUserHasPermissionOnAff(UserId, affId);
        }

        protected ResultInfo NotHavePermissionOnAffResult
            => new ResultInfo { Code = Code.ErrCodeAccessDenied, Message = Lang.NotAuthorizedAff };

        private void CheckPendingAff(int affId, string status = null)
        {
            if (string.IsNullOrEmpty(status))
            {
                var affInfo = AffiliateService.GetAffiliateBasicInfo(affId);
                IsPending = AffStatus.IsPendingAff(affInfo?.Status);
            }
            else
            {
                IsPending = AffStatus.IsPendingAff(status);
            }
        }
    }
}
