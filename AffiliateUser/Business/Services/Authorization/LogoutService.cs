using System;
using System.Web;
using System.Web.Security;

namespace AffiliateUser.Business.Services.Authorization
{
    public class LogoutService
    {
        public void Logout(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            string CurrentLang = AppSettings.Session.Language;
            try
            {
                //clear online list
                if (AppSettings.HasLogin) DataHandler.Auth.Logout(AppSettings.Session.OnUser);
                AuthHelper.ClearSession(context);
            }
            catch (Exception ex)
            {
                Fanex.Logging.LogService.Error(ex.Message);
            }

            AppSettings.Session.Language = CurrentLang;
            AppSettings.Session.Flag = CurrentLang;
            context.Response.Redirect(FormsAuthentication.LoginUrl);
        }
    }
}