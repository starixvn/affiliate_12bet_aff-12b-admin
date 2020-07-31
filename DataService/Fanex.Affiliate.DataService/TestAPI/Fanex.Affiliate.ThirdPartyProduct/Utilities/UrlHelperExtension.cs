using System.Configuration;
using System.Web.Mvc;

namespace Fanex.Affiliate.ThirdPartyProduct.Utilities
{
    public static class UrlHelperExtension
    {
        public static string VersionedLink(this UrlHelper urlHelper, string virtualPath)
        {
            var jsVersion = ConfigurationManager.AppSettings["IAppSetting"];
            return urlHelper.Content(virtualPath) + "?v=" + jsVersion;
        }
    }
}
