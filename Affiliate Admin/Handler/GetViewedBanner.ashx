<%@ WebHandler Language="C#" Class="GetViewedBanner" %>
using System;
using System.Web;
using DNAide.Core;
using System.Web.SessionState;
using System.Text;
using DNAide.Data;
using System.Data;

public class GetViewedBanner : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.UrlReferrer != null)
        {
            IBCHandler.InitializeCulture(HttpContext.Current);
            context.Response.ContentType = "text/plain";
            int bannerID = Convert.ToInt32(context.Request["bannerid"].Trim());
            int noRow = Convert.ToInt32(context.Request["noRow"].Trim());             
            context.Response.Write(GenerateHTML.Banner.GetViewedBanner(bannerID,noRow));
        }

    }
   
    public bool IsReusable {
        get {
            return false;
        }
    }

}
