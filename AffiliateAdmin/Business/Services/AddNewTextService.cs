using System.Web;

namespace AffiliateAdmin.Business.Services
{
    public class AddNewTextService
    {
        public void AddNewText(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                IBCHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/html";

                string textvalue = context.Request.Params["textvalue"];
                string category = context.Request.Params["slCategory"];
                string language = context.Request.Params["slLanguage"];
                string currency = context.Request.Params["slCurrency"];
                string deviceType = context.Request.Params["slDevice"];
                string err = string.Empty;
                int errCode = 0;
                int bannerID = DataHandler.Text.InsText(textvalue, category, language, currency, ref errCode, ref err, deviceType);

                if (!string.IsNullOrEmpty(err))
                {
                    context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + err + "');</script>");
                    return;
                }
                context.Response.Write("<script type=\"text/javascript\">alert('" + Resources.Lang.AddTextSuccessful + "');parent.Cancelfunction();</script>");
            }
        }
    }
}