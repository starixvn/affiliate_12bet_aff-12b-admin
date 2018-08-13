using System;
using System.Net;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class ValidateAntiForgeryHeaderAttribute : FilterAttribute, IAuthorizationFilter
    {
        private const string KeyName = "__RequestVerificationToken";

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            string clientToken = filterContext.RequestContext.HttpContext.Request.Headers.Get(KeyName);
            if (clientToken == null)
            {
                UnauthorizeAccessHandler(filterContext);
                return;
            }

            string serverToken = filterContext.HttpContext.Request.Cookies.Get(KeyName).Value;
            if (serverToken == null)
            {
                UnauthorizeAccessHandler(filterContext);
                return;
            }

            System.Web.Helpers.AntiForgery.Validate(serverToken, clientToken);
        }

        private static void UnauthorizeAccessHandler(AuthorizationContext context)
        {
            context.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden, "Access Denied");
        }
    }
}
