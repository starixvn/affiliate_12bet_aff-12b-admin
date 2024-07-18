namespace Fanex.Affiliate.User.Core.Utilities
{
    using Fanex.Affiliate.User.Core.Constants;
    using Fanex.Affiliate.User.Core.Extensions;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;

    public static class SiteDomainHelper
    {
        private const string CANONICAL_DOMAIN_NAME = "12betaffiliate.com";

        [SuppressMessage("Sonar Code Smell", "S3995:UriReturnValuesShouldNotBeStrings", Justification = "Reviewed. Suppression is OK here.")]
        public static string GetRootUrl(Controller controller, IAppSetting appSetting = null)
        {
            var setting = appSetting ?? DependencyResolver.Current.GetService<IAppSetting>();
            var isUatOrProEnv = !setting.IsDev;
            var requestUrl = controller.Request.Url;

            return GetScheme(controller.Request, setting.IsProduction) + Uri.SchemeDelimiter + requestUrl.Host +
                (requestUrl.IsDefaultPort || isUatOrProEnv ? string.Empty : ":" + requestUrl.Port) + controller.Url.RenderLanguageContent("~/");
        }

        public static string GenCanonialSeoLink(string path)
        {
            var currentLangName = Thread.CurrentThread.CurrentCulture.Name;
            var shortLangName = SystemLanguage.GetShortedCodeForTracking(currentLangName).ToLowerInvariant();
            if (currentLangName == "ja-JP")
            {
                shortLangName = "en";
            }
            var uriBuilder = new UriBuilder
            {
                Scheme = "https",
                Host = CANONICAL_DOMAIN_NAME,
                Port = -1,
                Path = path,
                Query = $"lang={shortLangName}"
            };
            return uriBuilder.ToString();
        }

        private static string GetScheme(HttpRequestBase request, bool isProduction)
        {
            return isProduction ? request.Url.Scheme : GetSchemeByRequest(request);
        }

        private static string GetSchemeByRequest(HttpRequestBase request)
        {
            var isSecure = request.IsSecureConnection || request.ServerVariables["HTTP_X_FORWARDED_PROTO"].MatchWith(Uri.UriSchemeHttps);

            return isSecure ? Uri.UriSchemeHttps : Uri.UriSchemeHttp;
        }
    }
}