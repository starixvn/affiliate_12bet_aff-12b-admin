using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Configurations;

namespace AffiliateAdmin
{
    public class Global : System.Web.HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            MvcHandler.DisableMvcResponseHeader = true;

            IoCConfig.Register();
       
            Fanex.Data.Configuration.DbSettingProviderManager.Current.Start();

            AreaRegistration.RegisterAllAreas();
            LoggingConfig.RegisterSentry();
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

            AffiliateAdmin.Business.Helpers.LogHelper.WriteError(ex);
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