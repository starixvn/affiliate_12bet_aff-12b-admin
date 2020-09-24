using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO.AdminRole;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Repository.AdminRole;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Fanex.Affiliate.Admin.Core.Services
{
    public class AuthService : IAuthService
    {
        private IAuthRepository AuthRepo { get; }
        private IAdminInfoRepository AdminUserRepo { get; }

        public AuthService(IAuthRepository authRepository, IAdminInfoRepository adminInfoRepository)
        {
            AuthRepo = authRepository;
            AdminUserRepo = adminInfoRepository;
        }

        public IEnumerable<Claim> GetClaims(int userId)
        {
            var account = AdminUserRepo.GetUserInfo(userId).AdminInfo;
            return BuildClaims(account);
        }

        private IEnumerable<Claim> BuildClaims(AdminInfo account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.UserId.ToString()),
                new Claim(ClaimTypes.GivenName, account.UserName),
            };

            var permissions = AuthRepo.GetPermissionList(account.UserId);
            claims.AddRange(permissions.Select(p => BuildClaimFromPermission(p)));

            return claims;
        }

        private static Claim BuildClaimFromPermission(UserPermission p)
        {
            var claimValue = p.IsHost ? Function.WritePermission : string.Empty;
            var claim = new Claim(p.FunctionName, claimValue);
            return claim;
        }
    }
}
