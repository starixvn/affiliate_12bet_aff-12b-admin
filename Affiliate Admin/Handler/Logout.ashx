<%@ WebHandler Language="C#" Class="Logout" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
public class Logout : IHttpHandler,IRequiresSessionState 
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/html";
        try
        {
            DataHandler.Login.Logout(context);            
        }
        catch { }
        context.Session.Clear();
        FormsAuthentication.SignOut();
        context.Response.Write("<html><head></head><body><script type='text/javascript'>top.location.href='" + FormsAuthentication.LoginUrl + "';</script></body></html>");        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
