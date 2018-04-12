using System.Linq;
using System.Web;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public static class RequestHelper
    {
        public static string GetClientIPAddress()
        {
            string ips = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            string ip = ips == null ?
                HttpContext.Current.Request.UserHostAddress :
                ips.Split(',').First().Trim();

            if (ip == "::1")
            {
                ip = "127.0.0.1";
            }

            return ip;
        }
    }
}