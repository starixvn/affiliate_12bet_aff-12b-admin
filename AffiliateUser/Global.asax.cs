using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AffiliateUser
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Fanex.Data.Configuration.DbSettingProviderManager.Current.Start();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Request.AcceptTypes != null && Request.AcceptTypes.Contains("text/html") &&
                Request.Url.Host.Equals(AppSettings.RedirectionEuHost, StringComparison.OrdinalIgnoreCase))
            {
                string ipAddress = Common.GetIPAddress();
                if (DataHandler.Common.IsNeedRedirect(ipAddress))
                {
                    Response.Redirect(AppSettings.RedirectionEuTarget);
                }
            }
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
                            "\r\nDOMAIN: " + HttpContext.Current.Request.Headers["Host"] +
                            "\r\nFORM: " + Request.Form.ToString() +
                            "\r\nUrl: " + Request.RawUrl +
                            "\r\nReferrer: " + Request.UrlReferrer +
                            "\r\nQUERYSTRING: " + Request.QueryString.ToString() +
                            "\r\nTARGETSITE: " + ex.TargetSite +
                            "\r\nSTACKTRACE: " + ex.StackTrace +
                            "\r\nBrowser:" + Request.Browser.Type.ToString() +
                            "\r\nBrowserVer:" + Request.Browser.Version.ToString() +
                            "\r\nAgent:" + Request.UserAgent.ToString();

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