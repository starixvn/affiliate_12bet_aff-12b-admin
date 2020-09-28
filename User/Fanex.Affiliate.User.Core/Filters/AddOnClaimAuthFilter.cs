using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using static Fanex.Affiliate.User.Core.Constants.SessionKey;

namespace Fanex.Affiliate.User.Core.Filters
{
    public class AddOnClaimAuthFilter : IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            var principal = filterContext.Principal as ClaimsPrincipal;
            var claimIdentities = session[ClaimIdentitiesKey] as IEnumerable<ClaimsIdentity>;
            if (claimIdentities != null)
            {
                principal.AddIdentities(claimIdentities);
            }    

        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext) { }
    }
}
