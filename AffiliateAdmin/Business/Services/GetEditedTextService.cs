using System;
using System.Web;

namespace AffiliateAdmin.Business.Services
{
    public class GetEditedTextService
    {
        public void GetEditedText(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                IBCHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";
                int textID = Convert.ToInt32(context.Request["textid"].Trim());
                int noRow = Convert.ToInt32(context.Request["noRow"].Trim());
                var deviceType = context.Request["deviceType"];
                context.Response.Write(GenerateHTML.Text.GetEditedText(textID, noRow, deviceType));
            }
        }
    }
}