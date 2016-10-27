namespace Affiliate.Infrastructure.Helpers
{
    using System;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Security;

    public static class AntiForgeryHelper
    {
        public const string AntiXsrfTokenKey = "__RequestVerificationToken";

        public static string GenerateXsrfFormToken(HttpContext context)
        {
            var requestCookie = context.Request.Cookies[AntiXsrfTokenKey];
            string formToken;

            if (requestCookie != null)
            {
                string cookieToken;
                AntiForgery.GetTokens(requestCookie.Value, out cookieToken, out formToken);
            }
            else
            {
                string cookieToken;
                AntiForgery.GetTokens(null, out cookieToken, out formToken);
                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = cookieToken,
                    Expires = DateTime.Now.AddMinutes(10)
                };

                if (FormsAuthentication.RequireSSL && context.Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }

                context.Response.Cookies.Set(responseCookie);
            }

            return formToken;
        }

        public static void ValidateXsrfToken(HttpContext context)
        {
            if (context.Request.RequestType != "GET")
            {
                var requestCookie = context.Request.Cookies[AntiXsrfTokenKey];

                var cookieTokenValue = requestCookie != null ? requestCookie.Value : string.Empty;

                AntiForgery.Validate(cookieTokenValue, context.Request["X-CSRF-Token"]);
            }
        }
    }
}