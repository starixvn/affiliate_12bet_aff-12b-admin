using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Fanex.Data;
using Fanex.Data.OldConnectionStringProvider;

namespace Fanex.Affiliate.User.Site
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            IoCConfig.Register();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DbSettingProviderManager
              .StartNewSession()
              .Use(new OldConnectionStringProvider())
              .UseDefaultDbSettingProvider(
                resourcePath: @"~\App_Data\SpConfig/SpName.xml",
                ignoreRedundantParameters: false,
                enableWatching: true)
              .Run();
        }
    }
}