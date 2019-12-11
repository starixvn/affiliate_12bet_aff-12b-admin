using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Affiliate.ThirdPartyProduct.Models;
using Affiliate.ThirdPartyProduct.Helper;
using System.Collections.Generic;

namespace Affiliate.ThirdPartyProduct
{
    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager 
    {
        private readonly HttpContextBase HttpContext;
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;
        public ApplicationSignInManager(HttpContextBase httpContext)
        {
            HttpContext = httpContext;
        }

        public Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool remmemberMe, bool shouldLockout = false)
        {
            if (UserHelper.IsValidateUser(userName, password))
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, userName));
                claims.Add(new Claim(ClaimTypes.Name, userName));

                var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                AuthenticationManager.SignIn(new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddDays(1),
                    IsPersistent = remmemberMe,
                    AllowRefresh = true,
                }, identity);

                return Task.FromResult(SignInStatus.Success);
            }
            else
                return Task.FromResult(SignInStatus.Failure);
        }
    }
}
