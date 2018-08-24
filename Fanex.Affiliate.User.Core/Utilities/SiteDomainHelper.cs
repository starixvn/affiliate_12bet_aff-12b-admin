namespace Fanex.Affiliate.User.Core.Utilities
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Web;
    using System.Web.Mvc;

    public static class SiteDomainHelper
    {
        [SuppressMessage("Sonar Code Smell", "S3995:UriReturnValuesShouldNotBeStrings", Justification = "Reviewed. Suppression is OK here.")]
        public static string GetRootUrl(Controller controller)
        {
            var appSetting = DependencyResolver.Current.GetService<IAppSetting>();
            var isUatOrProEnv = string.Equals("UAT", appSetting.Environment, StringComparison.OrdinalIgnoreCase) || appSetting.IsProduction;
            var requestUrl = controller.Request.Url;

            return GetScheme(controller.Request) + Uri.SchemeDelimiter + requestUrl.Host +
                (requestUrl.IsDefaultPort || isUatOrProEnv ? string.Empty : ":" + requestUrl.Port) + controller.Url.Content("~/");
        }

        private static string GetScheme(HttpRequestBase request)
        {
            var appSetting = DependencyResolver.Current.GetService<IAppSetting>();

            return (appSetting != null && appSetting.IsProduction) ?
                request.Url.Scheme :
                (IsSecure(request) ? Uri.UriSchemeHttps : Uri.UriSchemeHttp);
        }

        private static bool IsSecure(HttpRequestBase request)
        {
            return request.IsSecureConnection || String.Equals(request.ServerVariables["HTTP_X_FORWARDED_PROTO"], "https", StringComparison.OrdinalIgnoreCase);
        }
    }
}