<%@ WebHandler Language="C#" Class="GetClickStatisticsList" %>
using System;
using System.Web;
using DNAide.Core;
using System.Web.SessionState;
using System.Text;
using System.Data;
public class GetClickStatisticsList : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.UrlReferrer != null)
        {
            AffHandler.InitializeCulture(HttpContext.Current);
            context.Response.ContentType = "text/plain";            
            string trackingcode = context.Request.Params["params"];            
            context.Response.Write(GenerateHTML.TrackingSystem.GetStatisticsAllMonthsContentList(trackingcode));
          
        }
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}
