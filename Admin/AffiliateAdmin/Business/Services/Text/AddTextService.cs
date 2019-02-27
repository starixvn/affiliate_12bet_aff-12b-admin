using System.Web;
using AffiliateAdmin.Business.Models.Core;

namespace AffiliateAdmin.Business.Services.Text
{
    public class AddTextService
    {
        public ResultInfo AddNewText(HttpContext context)
        {
            string textvalue = context.Request.Params["textvalue"];
            string category = context.Request.Params["slCategory"];
            string language = context.Request.Params["slLanguage"];
            string currency = context.Request.Params["slCurrency"];
            string deviceType = context.Request.Params["slDevice"];
            string errorMessage = string.Empty;
            int errCode = 0;
            int bannerID = DataHandler.Text.InsText(textvalue, category, language, currency, ref errCode, ref errorMessage, deviceType);

            return new ResultInfo { Code = errCode, Message = errorMessage };
        }
    }
}