using System;
using System.Data;
using System.Text;
using System.Web;
using AffiliateAdmin.Business.DataAccess;

namespace AffiliateAdmin.Business.Services
{
    public class GetEmailContentService
    {
        public void GetEmailContent(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                IBCHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";

                int mid = -1;
                try
                {
                    mid = Convert.ToInt32(context.Request.Params["params"]);
                }
                catch (Exception e)
                {
                    context.Response.Write(e);
                }

                StringBuilder result = new StringBuilder();
                DbResult r = DataHandler.EmailHistory.EmailHistorySel(mid, string.Empty, string.Empty, string.Empty, null, null, AppSettings.PageSize, 1);

                if (Convert.ToInt32(r.Outputs["nrows"]) > 0)
                {
                    DataRow row = r.ResultSet.Tables[0].Rows[0];
                    result.Append(row["subject"].ToString());
                    result.Append("$$$");
                    result.Append(row["sender"].ToString());
                    result.Append("$$$");
                    result.Append(row["email"].ToString());
                    result.Append("$$$");
                    result.Append(row["createdate"].ToString());
                    result.Append("$$$");
                    result.Append(row["content"].ToString());
                    result.Append("$$$");
                    result.Append(row["mid"].ToString());
                }

                context.Response.Write(result.ToString());
            }
        }
    }
}