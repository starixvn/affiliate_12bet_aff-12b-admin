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
                context.Response.Write(GenerateHTML.TrackingSystem.GetStatisticsAllMonthsContentList(AppSettings.Session.AffId, trackingcode));
            }
        }
    }
}