using Fanex.Affiliate.Admin.Core.Constants;
using System;
using System.Security.Claims;

namespace Fanex.Affiliate.Admin.Core.Extensions
{
    public static class ClaimPrincipalExtension
    {
        public static bool HasClaim(this ClaimsPrincipal claims, string claimType)
            => claims.HasClaim(c => c.Type.Equals(claimType, StringComparison.OrdinalIgnoreCase));

        public static bool HasWriteClaim(this ClaimsPrincipal claims, string claimType)
            => claims.HasClaim(claimType, Function.WritePermission);
    }
}
