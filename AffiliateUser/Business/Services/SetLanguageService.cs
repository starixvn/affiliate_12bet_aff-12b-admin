using System;
using System.Web;

namespace AffiliateUser.Business.Services
{
    public class SetLanguageService
    {
        public void SetLanguage(HttpContext context)
        {
            context = AffHandler.InitializeCulture(context);
            try
            {
                string strLang = context.Request["lang"];
                AppSettings.Session.Flag = strLang;
                AppSettings.Session.Language = strLang;
                context.Response.Write(strLang);
            }
            catch (Exception ex)
            {
                Fanex.Logging.LogService.Error(ex.Message);
                context.Response.Write("");
            }
        }
    }
}