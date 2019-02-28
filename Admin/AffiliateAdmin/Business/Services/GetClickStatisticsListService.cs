using System;
using System.Web;

namespace AffiliateAdmin.Business.Services
{
    public class GetClickStatisticsListService
    {
        public void GetClickStatisticsList(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                IBCHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";
                string trackingcode = context.Request.Params["params"];
                int affId = string.IsNullOrEmpty(context.Request.Params["affId"]) ? AppSettings.Session.AffId : Convert.ToInt32(context.Request.Params["affId"]);
                context.Response.Write(GenerateHTML.TrackingSystem.GetStatisticsAllMonthsContentList(affId, trackingcode));
            }
        }
    }
}