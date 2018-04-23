using System;
using System.Web;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class CookieService : ICookieService
    {
        public void ClearAllCookies()
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

        public string GetCookie(string name)
        {
            var cookie = HttpContext.Current.Request.Cookies.Get(name);
            if (cookie != null)
            {
                return cookie.Value;
            }

            return string.Empty;
        }

        public void RemoveCookie(string name)
        {
            var cookie = HttpContext.Current.Response.Cookies.Get(name);

            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
            }
        }

        public void SetCookie(string name, string value)
        {
            var cookie = new HttpCookie(name, value);

            var cache = HttpContext.Current.Response.Cache;
            cache.SetNoStore();
            cache.SetExpires(DateTime.Now.AddMinutes(-1));
            cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cookies.Remove(name);
            HttpContext.Current.Response.SetCookie(cookie);
        }
    }
}
