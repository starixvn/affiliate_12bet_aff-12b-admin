using System;
using System.Web;

namespace AffiliateAdmin.Business.Services
{
    public class GetViewedBannerService
    {
        public void GetViewedBanner(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                IBCHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";
                int bannerID = Convert.ToInt32(context.Request["bannerid"].Trim());
                int noRow = Convert.ToInt32(context.Request["noRow"].Trim());
                context.Response.Write(GenerateHTML.Banner.GetViewedBanner(bannerID, noRow));
            }
        }
    }
}