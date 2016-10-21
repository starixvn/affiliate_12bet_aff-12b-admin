using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Security;

namespace AffiliateUser.Business.Services.Authorization
{
    public static class AuthHelper
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
                    Value = cookieToken
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

        public static void AddAuthToken(HttpContext context)
        {
            var guid = Guid.NewGuid().ToString();
            AppSettings.Session.AuthToken = guid;
            context.Response.Cookies.Add(new HttpCookie("AuthToken", guid));
        }

        public static void ClearSession(HttpContext context)
        {
            context.Session.Clear();
            context.Session.Abandon();
            context.Session.RemoveAll();

            if (context.Request.Cookies["ASP.NET_SessionId"] != null)
            {
                var sessionCookie = context.Response.Cookies["ASP.NET_SessionId"];
                sessionCookie.Value = string.Empty;
                sessionCookie.Expires = DateTime.Now.AddMonths(-20);
            }

            if (context.Request.Cookies["AuthToken"] != null)
            {
                var authCookie = context.Response.Cookies["AuthToken"];
                authCookie.Value = string.Empty;
                authCookie.Expires = DateTime.Now.AddMonths(-20);
            }
        }
    }
}