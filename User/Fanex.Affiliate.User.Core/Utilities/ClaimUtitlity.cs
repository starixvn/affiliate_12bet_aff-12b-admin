using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.CoreService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;

using System.Web;
using System.Web.Mvc;

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

        public static bool HasClaimOnlyInternalLogin(string claimType)
        {
            var sessionService = DependencyResolver.Current.GetService<ISessionService>();
            if(!sessionService.IsInternal)
            {
                return true;
            }

            return HasClaim(claimType);
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
