using System;
using System.Globalization;
using System.Web;
using DNAide.Core;

namespace AffiliateAdmin.Business.Services
{
    public class GetStatisticsDetailNewService
    {
        public void GetStatisticsDetail(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                context.Response.ContentType = "text/plain";
                CultureInfo culture = new CultureInfo("en-US");
                DateTime fDate = CastAide.AsDateTime(context.Request["fdate"], culture);
                DateTime tDate = CastAide.AsDateTime(context.Request["tdate"], culture);
                string domain = context.Request["domain"];
                string sportCode = context.Request["sportCode"];
                string languageCode = context.Request["languageCode"];
                string deviceType = Common.GetDeviceType(context.Request["deviceType"]);
                int pageSize = CastAide.AsInt32(context.Request["pageSize"]);
                int pageIndex = CastAide.AsInt32(context.Request["pageIndex"]);
                var affId = string.IsNullOrEmpty(context.Request["affId"]) ? AppSettings.Session.AffId : CastAide.AsInt32(context.Request["affId"]);

                var result = GenerateHTML.TrackingSystem.GetStatisticsDetail_New(affId, fDate, tDate, domain, sportCode, languageCode, pageSize, pageIndex, deviceType);

                context.Response.Write(result);
            }
        }
    }
}