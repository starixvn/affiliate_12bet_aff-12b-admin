namespace Fanex.Affiliate.Link
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Data.Configuration;

    /// <summary>
    /// Global for Affiliate link.
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            DbSettingProviderManager.Current.Start();

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
            //LogService.Error("Affiliate Link Error!" + Environment.NewLine + message + Environment.NewLine + Request.Url);
       }
    }
}