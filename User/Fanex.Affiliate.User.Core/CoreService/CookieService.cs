using System;
using System.Web;
using System.Web.Security;

namespace Fanex.Affiliate.User.Core.CoreService
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

    }
}
