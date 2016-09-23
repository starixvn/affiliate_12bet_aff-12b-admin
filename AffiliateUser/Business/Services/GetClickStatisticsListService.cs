using System.Web;

namespace AffiliateUser.Business.Services
{
    public class GetClickStatisticsListService
    {
        public void GetClickStatisticsList(HttpContext context)
        {
            AffHandler.InitializeCulture(HttpContext.Current);
            context.Response.ContentType = "text/plain";
            string trackingcode = context.Request.Params["params"];
            context.Response.Write(GenerateHTML.TrackingSystem.GetStatisticsAllMonthsContentList(trackingcode));
        }
    }
}