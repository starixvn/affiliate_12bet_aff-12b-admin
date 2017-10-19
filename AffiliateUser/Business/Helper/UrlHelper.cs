using System;
using System.Web;

namespace AffiliateUser.Business.Helper
{
    public static class UrlHelper
    {
        public static bool IsSecureConnection()
        {
            return HttpContext.Current.Request.IsSecureConnection ||
                string.Equals(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_PROTO"], "https", StringComparison.OrdinalIgnoreCase);
        }

        public static string GetProtocol()
        {
            return IsSecureConnection() ? Uri.UriSchemeHttps : Uri.UriSchemeHttp;
        }

        public static string GetUrlFromHostNamePattern(string hostNamePattern)
        {
            var hostName = HttpContext.Current.Request.Url.Host;
            if (string.Equals(hostName, "localhost", StringComparison.OrdinalIgnoreCase))
            {
                return hostNamePattern;
            }

            var protocol = GetProtocol();

            return AppSettings.LocalTest ?
                string.Format(hostNamePattern, protocol, HttpContext.Current.Request.Url.Port) :
                string.Format(hostNamePattern, protocol);
        }
    }
}