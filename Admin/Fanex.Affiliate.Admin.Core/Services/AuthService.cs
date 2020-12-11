using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Repository.AdminRole;
using Fanex.Affiliate.Admin.Core.Repository.Affiliate;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.UI.WebControls.Expressions;

namespace Fanex.Affiliate.Admin.Core.Services
{
    public class AuthService : IAuthService
    {
        private IAuthRepository AuthRepo { get; }
        private IAdminInfoRepository AdminUserRepo { get; }
        private IAffiliateInfoRepository AffRepo { get; }

        public AuthService(IAuthRepository authRepository,
            IAdminInfoRepository adminInfoRepository,
            IAffiliateInfoRepository affRepository)
        {
            AuthRepo = authRepository;
            AdminUserRepo = adminInfoRepository;
            AffRepo = affRepository;
        }

        public IEnumerable<Claim> GetClaims(int userId)
        {
            var account = AdminUserRepo.GetUserInfo(userId);
            return BuildClaims(account);
        }

        public bool IsUserHasPermissionOnAff(int userId, int affId)
            => AuthRepo.CheckIfAffBeInChargeByUser(userId, affId);

        private IEnumerable<Claim> BuildClaims(AdminInforResultEntity adminAcc)
        {
            var accountInfo = adminAcc.AdminInfo;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, accountInfo.UserId.ToString()),
                new Claim(ClaimTypes.GivenName, accountInfo.UserName),
            };

            var permissions = AuthRepo.GetPermissionList(accountInfo.UserId);
            claims.AddRange(permissions.Select(p => BuildClaimFromPermission(p)));

            var inChargeMarkets = adminAcc.MarketList.Where(m => m.HasPermission);
            claims.AddRange(inChargeMarkets.Select(m => BuildClaimFromMarket(m)));

            return claims;
        }

        private static Claim BuildClaimFromPermission(UserPermission p)
        {
            var claimValue = p.IsHost ? Function.WritePermission : string.Empty;
            var claim = new Claim(p.FunctionName, claimValue);
            return claim;
        }

        private static Claim BuildClaimFromMarket(MarketInfo m)
            => new Claim(Function.InChargeMarket, m.MarketId);
    }
}
