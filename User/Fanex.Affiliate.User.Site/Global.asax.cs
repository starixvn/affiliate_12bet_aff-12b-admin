using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using Fanex.Data;
using Fanex.Data.OldConnectionStringProvider;
using Fanex.Affiliate.User.Site.App_Start;

namespace Fanex.Affiliate.User.Site
{
    public class MvcApplication : System.Web.HttpApplication
    {
        [SuppressMessage("Sonar Code Smell", "S2325:Methods and properties should be static", Justification = "Reviewed.")]
        protected void Application_Start()
        {
            var env = ConfigurationManager.AppSettings["Environment"];
            IoCConfig.Register();
            AreaRegistration.RegisterAllAreas();

            if (env != "LOCAL")
            {
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            }

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            MvcHandler.DisableMvcResponseHeader = true;
            AntiForgeryConfig.RequireSsl = false;
            AntiForgeryConfig.CookieName = "__CSRFAFFSYS";

            DbSettingProviderManager
              .StartNewSession()
              .Use(new OldConnectionStringProvider())
              .UseDefaultDbSettingProvider(
                resourcePath: @"~\App_Data\SpConfig\SpName.xml",
                enableWatching: true)
              .Run();

            LoggingConfig.RegisterSentry();
        }

        [SuppressMessage("Sonar Code Smell", "S2325:Methods and properties should be static", Justification = "Reviewed.")]
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var application = sender as HttpApplication;
            if (application != null && application.Context != null)
            {
                application.Context.Response.Headers.Remove("Server");
            }
        }

    }
}