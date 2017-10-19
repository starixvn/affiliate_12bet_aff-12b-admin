using System;
using System.Text.RegularExpressions;
using System.Web;

namespace AffiliateAdmin.Business.Helpers
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

        public static string GetLeftPart()
        {
            return GetProtocol() + Uri.SchemeDelimiter;
        }

        public static string GetUrlFromHostName(string hostName)
        {
            var pattern = @"^(http:\/\/|https:\/\/)";
            hostName = Regex.Replace(hostName, pattern, string.Empty);

            return GetLeftPart() + hostName;
        }
    }
}