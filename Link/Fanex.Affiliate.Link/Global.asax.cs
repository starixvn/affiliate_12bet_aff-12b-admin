namespace Fanex.Affiliate.Link
{
    using Fanex.Affiliate.Link.Helpers;
    using Fanex.Data;
    using Fanex.Data.SqlServer;
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Global for Affiliate link.
    /// </summary>
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

            LogHelper.WriteError(exception, $"Request url: {Request.Url}");
        }
    }
}