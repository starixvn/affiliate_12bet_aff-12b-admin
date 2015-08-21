<%@ WebHandler Language="C#" Class="LogOut" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using DNAide.Web;

public class LogOut : IHttpHandler, IRequiresSessionState {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/html";
        string CurrentLang = AppSettings.Session.Language;
        try
        {
            //clear online list
            if (AppSettings.HasLogin) DataHandler.Auth.Logout(AppSettings.Session.OnUser);
            context.Session.Clear();
        }
        catch (Exception ex)
        {
            Fanex.Logging.LogService.Error(ex.Message);
        }
        AppSettings.Session.Language = CurrentLang;
        AppSettings.Session.Flag = CurrentLang;
        context.Response.Redirect(FormsAuthentication.LoginUrl);       
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
