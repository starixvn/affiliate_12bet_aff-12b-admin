using System;
using System.Data.SqlClient;
using System.Web;

namespace AffiliateUser
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            /*  CacheUtil.GetLatestHotNews(null);*/
            // Code that runs on application startup            
         //   Sunplus.Agent.Configurations.AppSettings.Configuration.Load();
            // Code that runs on application startup
            Fanex.Data.Configuration.DbSettingProviderManager.Current.Start();   
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            Exception ex = Server.GetLastError().GetBaseException();
            if (ex is HttpException)
            {
                return;
            }

            string message = ex.Message +
                            "\r\nSOURCE: " + ex.Source +
                            "\r\nFORM: " + Request.Form.ToString() +
                            "\r\nQUERYSTRING: " + Request.QueryString.ToString() +
                            "\r\nTARGETSITE: " + ex.TargetSite +
                            "\r\nSTACKTRACE: " + ex.StackTrace;

            var sqlException = ex as SqlException;
            if (sqlException != null)
            {
                message += "\r\nProcedure: " + sqlException.Procedure +
                    "\r\nLine Number: " + sqlException.LineNumber +
                    "\r\nServer: " + sqlException.Server;
            }

            Fanex.Logging.LogService.Error(message, ex);
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}