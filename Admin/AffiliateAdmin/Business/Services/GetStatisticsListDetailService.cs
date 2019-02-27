using System;
using System.Text;
using System.Web;

namespace AffiliateAdmin.Business.Services
{
    public class GetStatisticsListDetailService
    {
        public void GetStatisticsList(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                IBCHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";

                string[] param = context.Request.Params["params"].Split(";".ToCharArray());
                int affId = string.IsNullOrEmpty(context.Request.Params["affId"]) ? AppSettings.Session.AffId : Convert.ToInt32(context.Request.Params["affId"]);

                string trackingcode = string.Empty;
                int month = 5, year = 2009;
                StringBuilder result = ValidateData(param, ref trackingcode, ref month, ref year);
                if (string.IsNullOrEmpty(result.ToString()))
                {
                    result.Append(GenerateHTML.TrackingSystem.GetStatisticsMonthDetailContent(affId, month, year, trackingcode));
                }

                context.Response.Write(result.ToString());
            }
        }

        private StringBuilder ValidateData(string[] param, ref string trackingcode, ref int month, ref int year)
        {
            StringBuilder err = new StringBuilder("");

            try
            {
                if (param.Length == 2)
                {
                    trackingcode = param[0].Trim();
                    DateTime dt = Convert.ToDateTime(param[1].Trim());
                    month = dt.Month;
                    year = dt.Year;
                }
            }
            catch (Exception e)
            {
                return err.Append(e.Message);
            }
            return err;
        }
    }
}