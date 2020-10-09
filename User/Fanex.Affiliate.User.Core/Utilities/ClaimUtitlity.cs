using Fanex.Affiliate.User.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;

using System.Web;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public static class ClaimUtitlity
    {
        public static bool HasClaim(string claimType)
        {
            var pricinpal = HttpContext.Current.User as ClaimsPrincipal;
            if (pricinpal != null)
            {
                return pricinpal.HasClaim(c => claimType.Equals(c.Type, StringComparison.InvariantCultureIgnoreCase));
            }
            return false;
        }

        public static bool HasWriteClaim(string claimType)
        {
            var pricinpal = HttpContext.Current.User as ClaimsPrincipal;
            if (pricinpal != null)
            {
                return pricinpal.HasClaim(claimType, ClaimConstant.WriteClaimValue);
            }
            return false;
        }
    }
}
