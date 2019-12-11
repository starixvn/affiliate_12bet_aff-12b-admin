using Fanex.Affiliate.Admin.Core.Configurations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
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
            Fanex.Data.Configuration.DbSettingProviderManager.Current.Start();
            IoCConfig.Register();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            LoggingConfig.RegisterSentry();
        }

        protected void Application_EndRequest(Object sender, EventArgs e)
        {
            if (HttpContext.Current.Response.Status.StartsWith("401"))
            {
                HttpContext.Current.Response.ClearContent();
                Response.Redirect("/Account/Login");
            }
        }
    }
}
