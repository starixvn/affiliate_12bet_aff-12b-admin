using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Utilities;

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

            LogHelper.WriteError(ex);
        }
    }
}