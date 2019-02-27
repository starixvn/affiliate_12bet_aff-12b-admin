using System;
using System.Web;
using AffiliateAdmin.Business.Models.Core;

namespace AffiliateAdmin.Business.Services.Text
{
    public class UpdateTextService
    {
        public ResultInfo Update(HttpContext context)
        {
            string idbanner = context.Request.Params["hdBannerID"];
            string textvalue = context.Request.Params["textvalue"];
            string category = context.Request.Params["slCategory"];
            string language = context.Request.Params["slLanguage"];
            string currency = context.Request.Params["slCurrency"];
            string deviceType = context.Request.Params["slDevice"];

            string err = DataHandler.Text.UpdText(Convert.ToInt32(idbanner), textvalue, category, language, currency, deviceType);

            return new ResultInfo { Message = err };
        }
    }
}