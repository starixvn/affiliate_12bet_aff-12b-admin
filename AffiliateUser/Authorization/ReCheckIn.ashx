<%@ WebHandler Language="C#" Class="ReCheckIn" %>
using System;
using System.Web;
using System.Web.SessionState;
public class ReCheckIn : IHttpHandler, IRequiresSessionState {

    public void ProcessRequest (HttpContext context) {
        if(AppSettings.LocalTest)
        {
            return;
        }

        context.Response.ContentType = "text/html";
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        if (AppSettings.Session.OnUser != -1)
        {
            if (DataHandler.Auth.ReCheckin())
            {
                context.Response.Write("Expired");
            }
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
