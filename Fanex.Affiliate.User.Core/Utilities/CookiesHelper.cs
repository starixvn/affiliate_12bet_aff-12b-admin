using System;
using System.Web;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public static class CookiesHelper
    {
        public static string GetCookies(string name)
        {
            var cookies = HttpContext.Current.Request.Cookies.Get(name);
            if (cookies != null)
            {
                return cookies.Value;
            }

            return string.Empty;
        }

        public static void SetCookies(string name, string value)
        {
            var cookie = new HttpCookie(name, value);

            var cache = HttpContext.Current.Response.Cache;
            cache.SetNoStore();
            cache.SetExpires(DateTime.Now.AddMinutes(-1));
            cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cookies.Remove(name);
            HttpContext.Current.Response.SetCookie(cookie);
        }

        public static void RemoveCookie(string name)
        {
            HttpContext.Current.Response.Cookies.Remove(name);
        }

        public static void ClearAllCookies()
        {
            var cache = HttpContext.Current.Response.Cache;
            cache.SetNoStore();
            cache.SetExpires(DateTime.Now.AddMinutes(-1));
            cache.SetCacheability(HttpCacheability.NoCache);
            var cookies = HttpContext.Current.Request.Cookies.AllKeys;
            foreach (string cookie in cookies)
            {
                var item = new HttpCookie(cookie)
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                HttpContext.Current.Response.Cookies.Add(item);
            }
        }
    }
}
