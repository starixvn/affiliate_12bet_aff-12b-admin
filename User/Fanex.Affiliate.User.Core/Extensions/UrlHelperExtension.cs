using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.Utilities;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Fanex.Affiliate.User.Core.Extensions
{
    public static class UrlHelperExtension
    {
        private static readonly IAppSetting appSetting = DependencyResolver.Current.GetService<IAppSetting>();
        private static readonly ISessionService sessionService = DependencyResolver.Current.GetService<ISessionService>();

        public static string RenderLink(this UrlHelper urlHelper, string bundleVirtualPath)
        {
            var link = appSetting.IsLocal ? urlHelper.Content(bundleVirtualPath) :
                                            urlHelper.Content(Regex.Replace(bundleVirtualPath, @"\.(js|css)$", ".min.$1"));

            return link + "?v=" + appSetting.JsVersion;
        }

        public static string RenderLinkByJsVersion(this UrlHelper urlHelper, string bundleVirtualPath)
        {
            return urlHelper.Content(bundleVirtualPath) + "?v=" + appSetting.JsVersion;
        }

        public static string RenderLanguageContent(this UrlHelper urlHelper, string bundleVirtualPath)
        {
            var currentLanguage = sessionService.CurrentLanguage;
            var shortedCode = SystemLanguage.GetCurrentShortedLanguage(currentLanguage);
            
            return urlHelper.Content(Regex.Replace(bundleVirtualPath, @"^\~/", $"~/{shortedCode}/"));
        }
    }
}
