using System;
using System.Text;
using System.Web;

namespace AffiliateUser.Business.Services
{
    public class GetStatisticsListDetailService
    {
        public void GetStatisticsListDetail(HttpContext context)
        {
            AffHandler.InitializeCulture(HttpContext.Current);
            context.Response.ContentType = "text/plain";

            string[] param = context.Request.Params["params"].Split(";".ToCharArray());

            string trackingcode = string.Empty;
            int month = 5, year = 2009;
            StringBuilder result = ValidateData(param, ref trackingcode, ref month, ref year);
            if (string.IsNullOrEmpty(result.ToString()))
            {
                result.Append(GenerateHTML.TrackingSystem.GetStatisticsMonthDetailContent(month, year, trackingcode));
            }

            context.Response.Write(result.ToString());
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