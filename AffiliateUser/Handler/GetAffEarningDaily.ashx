<%@ WebHandler Language="C#" Class="GetSubCategory" %>
using System;
using System.Web;
using DNAide.Core;
using System.Web.SessionState;
using System.Text;
using System.Data;

public class GetSubCategory : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.UrlReferrer != null)
        {
            AffHandler.InitializeCulture(HttpContext.Current);
            context.Response.ContentType = "text/plain";
            
            int sportType = !string.IsNullOrEmpty(context.Request["sportType"]) ? CastAide.AsInt32(context.Request["sportType"]) : -1;
            DateTime _date = (context.Request["monthdate"] != null) ? DateTime.Parse(context.Request["monthdate"].ToString(), System.Globalization.CultureInfo.CurrentCulture) : DateTime.Now.AddDays(-1);

            context.Response.Write(GenerateHTML.DailyEarning.DailyEarningList(sportType, _date));
        }

    }
   
    public bool IsReusable {
        get {
            return false;
        }
    }

}
