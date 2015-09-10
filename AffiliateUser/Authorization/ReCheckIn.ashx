<%@ WebHandler Language="C#" Class="ReCheckIn" %>
using System;
using System.Web;
using System.Web.SessionState; 
public class ReCheckIn : IHttpHandler, IRequiresSessionState {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/html";
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        if (AppSettings.Session.OnUser != -1)
        {
            if (DataHandler.Auth.ReCheckin())
            {
                context.Response.Write("Expired");
            }
        }

        string startTime, endTime;
        bool isUM = Common.GetDownTime(out startTime, out endTime);
        if (isUM)
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
