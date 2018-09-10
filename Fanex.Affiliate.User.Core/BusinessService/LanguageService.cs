using System.Globalization;
using System.Threading;
using Fanex.Affiliate.User.Core.Constants;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class LanguageService : ILanguageService
    {
        public void SetCulture(string languageCode)
        {
            if (SystemLanguage.IsValid(languageCode))
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageCode);
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(languageCode);

                Thread.CurrentThread.CurrentCulture.DateTimeFormat = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
                Thread.CurrentThread.CurrentCulture.NumberFormat = CultureInfo.CreateSpecificCulture("en-US").NumberFormat;
            }
        }
    }
}
