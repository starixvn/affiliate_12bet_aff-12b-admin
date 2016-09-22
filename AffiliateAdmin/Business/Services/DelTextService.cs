using System.Web;

namespace AffiliateAdmin.Business.Services
{
    public class DelTextService
    {
        public void DelText(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                IBCHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";
                string listIdBanner = context.Request.Params["params"];
                string result = DataHandler.Text.DelDataText(listIdBanner);
                context.Response.Write(result);
            }
        }
    }
}