using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Data.SqlClient;

namespace AffiliateAdmin
{
    public class Global : System.Web.HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            Sunplus.Agent.Configurations.AppSettings.Configuration.Load();
            // Code that runs on application startup
            Fanex.Data.Configuration.DbSettingProviderManager.Current.Start();
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
        }

        void Application_Error(object sender, EventArgs e)
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

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            try
            {
                if (!User.Identity.IsAuthenticated || Request.RawUrl.IndexOf(".aspx") == -1 || string.IsNullOrWhiteSpace(AppSettings.Session.RoleAccess))
                    return;

                if (AppSettings.Session.RoleAccess.IndexOf("[All]", StringComparison.CurrentCultureIgnoreCase) != -1) return;

                string url;
                if (AppSettings.VirtualDir)
                {
                    url = Request.RawUrl.Substring(Request.RawUrl.IndexOf("/", 1) + 1);
                }
                else
                {
                    url = Request.RawUrl.Substring(1);
                }

                url = url.Substring(0, url.IndexOf(".aspx") + 5);

                if (Context.Cache["AccessPermission"] == null)
                    Common.InitAccessPermission();
                string pageRole = ((Dictionary<string, string>)Context.Cache["AccessPermission"]).ContainsKey(url) ? ((Dictionary<string, string>)Context.Cache["AccessPermission"])[url] : "";
                //HttpContext.Current.Response.Write("Raw url: " + Request.RawUrl.ToString() + "");
                if (string.Compare(pageRole, "[All]", true) == 0) return;
                if (AppSettings.Session.RoleAccess.IndexOf(pageRole) == -1)
                {
                    Response.Redirect("~/AccessDenied.aspx");
                }
            }
            catch
            {

            }
        }
    }
}