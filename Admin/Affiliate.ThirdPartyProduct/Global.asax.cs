using Fanex.Affiliate.ThirdPartyProduct.Site;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Affiliate.ThirdPartyProduct.TestRequest
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Fanex.Data.Configuration.DbSettingProviderManager.Current.Start();
            LoggingConfig.RegisterSentry();
        }


    }
}
