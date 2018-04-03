namespace Fanex.Affiliate.User.Core.Utilities
{
    using System;
    using System.Web.Mvc;

    public static class SiteDomainHelper
    {
        public static string GetRootUrl(Controller controller)
        {
            var requestUrl = controller.Request.Url;

            return requestUrl.Scheme + Uri.SchemeDelimiter + requestUrl.Host +
                (requestUrl.IsDefaultPort ? string.Empty : ":" + requestUrl.Port) + controller.Url.Content("~/");
        }
    }
}