using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public static class RequestHelper
    {
        [SuppressMessage("Sonar Code Smell", "S1313:MakeIpAddressConfigurable", Justification = "Reviewed. Suppression is OK here.")]
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