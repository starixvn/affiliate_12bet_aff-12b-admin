<%@ WebHandler Language="C#" Class="LogOff" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;

public class LogOff : IHttpHandler,IRequiresSessionState {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/html";
        context.Response.Cache.SetCacheability(HttpCacheability.Private);
        context.Session.Clear();
        FormsAuthentication.SignOut();
        context.Response.Redirect("~/Root.aspx");
    }
 
    public bool IsReusable {
        get {
            return true;
        }
    }

}
