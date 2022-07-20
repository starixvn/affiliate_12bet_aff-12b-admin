using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public static class RequestHelper
    {
        public static string GetClientIPAddress(HttpRequestBase httpRequest = null)
        {
            var request = httpRequest ?? new HttpRequestWrapper(HttpContext.Current.Request);
            var ips = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            var ip = ips == null ?
                request.UserHostAddress :
                ips.Split(',').First().Trim();

            if (ip == "::1")
            {
                ip = "127.0.0.1";
            }

            return ip;
        }
    }
}