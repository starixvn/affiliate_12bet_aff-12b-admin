using System;
using System.Web;
using DNAide.Core;

namespace AffiliateUser.Business.Services
{
    public class GetAffiliateEarningDailyService
    {
        public void GetAffiliateEarningDaily(HttpContext context)
        {
            AffHandler.InitializeCulture(HttpContext.Current);
            context.Response.ContentType = "text/plain";

            int sportType = !string.IsNullOrEmpty(context.Request["sportType"]) ? CastAide.AsInt32(context.Request["sportType"]) : -1;
            DateTime _date = (context.Request["monthdate"] != null) ? DateTime.Parse(context.Request["monthdate"].ToString(), System.Globalization.CultureInfo.CurrentCulture) : DateTime.Now.AddDays(-1);

            context.Response.Write(GenerateHTML.DailyEarning.DailyEarningList(sportType, _date));
        }
    }
}