using System.Text.RegularExpressions;
using System.Web;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public static class RequestHelper
    {
        private const string IpRegex = "((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\\.|$)){4}";

        public static string GetClientIPAddress(HttpRequestBase httpRequest = null)
        {
            var request = httpRequest ?? new HttpRequestWrapper(HttpContext.Current.Request);
            var ips = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            var ip = GetForwardedIPAddress(ips) ?? request.UserHostAddress;

            if (ip == "::1")
            {
                ip = "127.0.0.1";
            }

            return ip;
        }

        public static string GetValidOrEmptyClientIPAddress(HttpRequestBase httpRequest = null)
        {
            var ip = GetClientIPAddress(httpRequest);

            return Regex.IsMatch(ip, IpRegex) ? ip : string.Empty;
        }

        private static string GetForwardedIPAddress(string ips)
        {
            if (string.IsNullOrEmpty(ips))
            {
                return null;
            }

            var ipList = ips.Split(',');

            foreach (var ip in ipList)
            {
                if (Regex.IsMatch(ip, IpRegex))
                {
                    return ip;
                }
            }

            return null;
        }
    }
}