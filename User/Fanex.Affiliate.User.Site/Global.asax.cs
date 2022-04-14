using Fanex.Affiliate.User.Core.Filters;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Affiliate.User.Site.App_Start;
using Fanex.Data;
using Fanex.Data.SqlServer;
using System;
using System.Configuration;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

namespace Fanex.Affiliate.User.Site
{
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            var env = ConfigurationManager.AppSettings["Environment"];
            IoCConfig.Register();
            AreaRegistration.RegisterAllAreas();

            UMConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            if (env != "LOCAL")
            {
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            }
            GlobalFilters.Filters.Add(new AddOnClaimAuthFilter());

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            MvcHandler.DisableMvcResponseHeader = true;
            AntiForgeryConfig.RequireSsl = false;
            AntiForgeryConfig.CookieName = "__CSRFAFFSYS";

            DbSettingProviderManager
              .StartNewSession()
              .WithSqlServer("~/App_Data/SpConfig/SpName.xml")
              .Run();

            LoggingConfig.RegisterSentry();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var application = sender as HttpApplication;
            if (application != null && application.Context != null)
            {
                application.Context.Response.Headers.Remove("Server");
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            Exception ex = Server.GetLastError().GetBaseException();

            if (ex is HttpException)
            {
                return;
            }

            LogHelper.WriteError(ex);
        }
    }
}