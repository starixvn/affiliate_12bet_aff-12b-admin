using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AffiliateUser.Business.Helper;

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
            Exception exception = Server.GetLastError().GetBaseException();
            if (exception is HttpException)
            {
                return;
            }

            LogHelper.WriteError(exception);
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}