namespace Fanex.Affiliate.User.Core.Utilities
{
    using System;
    using System.Web;
    using System.Web.Mvc;

    public static class SiteDomainHelper
    {
        public static string GetRootUrl(Controller controller)
        {
            var requestUrl = controller.Request.Url;

            return GetScheme(controller.Request) + Uri.SchemeDelimiter + requestUrl.Host +
                (requestUrl.IsDefaultPort ? string.Empty : ":" + requestUrl.Port) + controller.Url.Content("~/");
        }

        private static string GetScheme(HttpRequestBase request)
        {
            var appSetting = DependencyResolver.Current.GetService<IAppSetting>();

            return appSetting.IsProduction ? request.Url.Scheme : (IsSecure(request) ? Uri.UriSchemeHttps : Uri.UriSchemeHttp);
        }

        private static bool IsSecure(HttpRequestBase request)
        {
            return request.IsSecureConnection || String.Equals(request.ServerVariables["HTTP_X_FORWARDED_PROTO"], "https", StringComparison.OrdinalIgnoreCase);
        }
    }
}