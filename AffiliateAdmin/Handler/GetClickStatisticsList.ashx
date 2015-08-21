<%@ WebHandler Language="C#" Class="GetClickStatisticsList" %>
using System;
using System.Web;
using DNAide.Core;
using System.Web.SessionState;
using System.Text;
using System.Data;
using AffiliateAdmin.Business.DataAccess;
public class GetClickStatisticsList : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.UrlReferrer != null)
        {
            IBCHandler.InitializeCulture(HttpContext.Current);
            context.Response.ContentType = "text/plain";            
            string trackingcode = context.Request.Params["params"];
            context.Response.Write(GenerateHTML.TrackingSystem.GetStatisticsAllMonthsContentList(AppSettings.Session.AffId,trackingcode));
          
        }
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}
