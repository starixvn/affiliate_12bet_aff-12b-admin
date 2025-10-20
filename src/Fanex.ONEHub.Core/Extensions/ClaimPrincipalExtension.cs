using Fanex.Affiliate.Admin.Core.Constants;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Fanex.Affiliate.Admin.Core.Extensions
{
    public static class ClaimPrincipalExtension
    {
        public static bool HasClaim(this ClaimsPrincipal claims, string claimType)
            => claims.HasClaim(c => c.Type.MatchWith(claimType));

        public static bool HasWriteClaim(this ClaimsPrincipal claims, string claimType)
            => claims.HasClaim(claimType, Function.WritePermission);

        public static string ToJson(this IEnumerable<Claim> claims)
        {
            var dic = claims.GroupBy(c => c.Type, c => c.Value)
                .ToDictionary(c => c.Key, c => c.AsEnumerable());
            return JsonConvert.SerializeObject(dic);
        }
    }
}
