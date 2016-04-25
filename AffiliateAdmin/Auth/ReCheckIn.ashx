<%@ WebHandler Language="C#" Class="ReCheckIn" %>

using System;
using System.Web;
using AffiliateAdmin.Business.DataAccess;
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
        
        if (!Common.IsPassUnderMaintenance())
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
