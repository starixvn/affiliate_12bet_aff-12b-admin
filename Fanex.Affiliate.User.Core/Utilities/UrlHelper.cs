using System;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public static class SiteDomainHelper
    {
        public static string GetRootUrl(Uri requestUrl)
        {
            return requestUrl.Scheme + Uri.SchemeDelimiter + requestUrl.Host +
                (requestUrl.IsDefaultPort ? string.Empty : ":" + requestUrl.Port);
        }
    }
}