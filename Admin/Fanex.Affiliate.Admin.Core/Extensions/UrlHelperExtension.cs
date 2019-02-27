using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Configurations;

namespace Fanex.Affiliate.Admin.Core.Extensions
{
    public static class UrlHelperExtension
    {
        private static IAppSetting appSetting = DependencyResolver.Current.GetService<IAppSetting>();

        public static string VersionedLink(this UrlHelper urlHelper, string virtualPath)
        {
            return urlHelper.Content(virtualPath) + "?v=" + appSetting.JsVersion;
        }
    }
}
