using System;
using System.Data;
using System.Text;
using System.Web;
using AffiliateAdmin.Business.DataAccess;

namespace AffiliateAdmin.Business.Services
{
    public class GetMessageService
    {
        public void GetMessage(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                IBCHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";

                int msgid = -1;
                try
                {
                    msgid = Convert.ToInt32(context.Request.Params["params"]);
                }
                catch (Exception e)
                {
                    context.Response.Write(e);
                }

                StringBuilder result = new StringBuilder();
                DbResult r = DataHandler.HotNews.GetHotNews(msgid);

                if (r.ResultSet.Tables[0].Rows.Count > 0)
                {
                    DataRow row = r.ResultSet.Tables[0].Rows[0];
                    result.Append(row["important"].ToString().ToLower());
                    result.Append("$$$");
                    result.Append(row["message_en"].ToString());
                    result.Append("$$$");
                    result.Append(row["message_cn"].ToString());
                    result.Append("$$$");
                    result.Append(row["message_tw"].ToString());
                    result.Append("$$$");
                    result.Append(row["message_th"].ToString());
                    result.Append("$$$");
                    result.Append(row["message_es"].ToString());
                    result.Append("$$$");
                    result.Append(row["msgid"].ToString());
                }

                context.Response.Write(result.ToString());
            }
        }
    }
}