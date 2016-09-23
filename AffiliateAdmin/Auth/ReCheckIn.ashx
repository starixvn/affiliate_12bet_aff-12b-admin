<%@ WebHandler Language="C#" Class="ReCheckIn" %>

using System.Web;
using System.Web.SessionState; 
public class ReCheckIn : IHttpHandler, IRequiresSessionState {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/html";
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (AppSettings.Session.OnUser > 0)
        {
            if (DataHandler.Login.ReCheckin(context))
                context.Response.Write("Expired");
        }
        else
        {
            context.Response.Write("Expired");
            return;
        }
        
        if (Common.IsUnderMaintenance())
        {
            context.Response.Write("UM");            
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
