using System;
using System.Web;
using Affiliate.Infrastructure.Helpers;
using DNAide.Core;

namespace AffiliateAdmin.Business.Services
{
    public class AdjustmentService
    {
        public void Adjustment(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            try
            {
                AntiForgeryHelper.ValidateXsrfToken(context);
            }
            catch
            {
                context.Response.Write("1@" + Resources.Lang.Err500);
                return;
            }

            int custid = CastAide.AsInt32(context.Request["custid"]);
            decimal dcAdjust = CastAide.AsDecimal(context.Request["adj"], 0);
            string description = context.Request["des"];
            if (dcAdjust == 0)
                return;
            try
            {
                Int64 transId = 0;
                string errMsg = string.Empty;
                int errCode = 0;
                errCode = DataHandler.Affiliate.Adjustment(dcAdjust, custid, AppSettings.Session.CustID, description, ref transId, ref errMsg);
                errMsg = (string.IsNullOrEmpty(errMsg) ? Resources.Lang.UpdateSucc : errMsg);
                context.Response.Write(errCode + "@" + errMsg);
            }
            catch (Exception ex)
            {
                context.Response.Write("1@" + ex.Message);
            }
        }
    }
}