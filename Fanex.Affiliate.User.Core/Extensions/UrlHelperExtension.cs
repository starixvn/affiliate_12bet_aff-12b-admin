using System.Text.RegularExpressions;
using System.Web.Mvc;
using Fanex.Affiliate.User.Core.Utilities;

namespace Fanex.Affiliate.User.Core.Extensions
{
    public static class UrlHelperExtension
    {
        private static IAppSetting appSetting = DependencyResolver.Current.GetService<IAppSetting>();

        public static string RenderLink(this UrlHelper urlHelper, string bundleVirtualPath)
        {
            var link = appSetting.IsLocal ? urlHelper.Content(bundleVirtualPath) :
                                            urlHelper.Content(Regex.Replace(bundleVirtualPath, @"\.(js|css)$", ".min.$1"));

            return link + "?v=" + appSetting.JsVersion;
        }
    }
}
