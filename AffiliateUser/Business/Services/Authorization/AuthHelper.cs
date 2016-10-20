using System;
using System.Web;

namespace AffiliateUser.Business.Services.Authorization
{
    public class AuthHelper
    {
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