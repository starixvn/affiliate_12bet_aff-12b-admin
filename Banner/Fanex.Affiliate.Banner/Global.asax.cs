namespace Affiliate.Banner
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using App_Start;
    using Fanex.Data;
    using Fanex.Data.SqlServer;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            DbSettingProviderManager
                .StartNewSession()
                .WithSqlServer("~/App_Data/SpNames.xml")
                .Run();
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            LoggingConfig.RegisterSentry();
        }

        /// <summary>
        /// Application Error.
        /// </summary>
        /// <param name="sender">Object sender.</param>
        /// <param name="e">Error arguments.</param>
        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            if (exception is HttpException)
            {
                return;
            }

            //var message = exception.Message + Environment.NewLine + exception.StackTrace;
            //LogService.Error("Affiliate Media Error!" + Environment.NewLine + message + Environment.NewLine + Request.Url);
        }
    }
}