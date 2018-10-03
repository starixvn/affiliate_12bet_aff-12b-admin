using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using Fanex.Affiliate.User.Core.Constants;
namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class LanguageService : ILanguageService
    {
        public string GetFavoriteLanguage()
        {
            var preferedUrlLang = HttpContext.Current.Request["lang"]?.Trim();
            var preferedLang = SystemLanguage.SupportedLanguage.FirstOrDefault(i => string.Equals(i.Value, preferedUrlLang, StringComparison.OrdinalIgnoreCase)).Key;

            if (!string.IsNullOrEmpty(preferedLang) && SystemLanguage.IsValid(preferedLang))
            {
                return preferedLang;
            }


            var langCookie = HttpContext.Current.Request.Cookies["afffavoritelang"]?.Value;

            return SystemLanguage.IsValid(langCookie) ? langCookie : SystemLanguage.DefaultLanguageCode;
        }


        public void SetCulture(string languageCode)
        {

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageCode);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(languageCode);

            Thread.CurrentThread.CurrentCulture.DateTimeFormat = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
            Thread.CurrentThread.CurrentCulture.NumberFormat = CultureInfo.CreateSpecificCulture("en-US").NumberFormat;

        }
    }
}
