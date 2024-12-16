using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Data.Repository;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace Fanex.Affiliate.Admin.Core.Services.Common
{
    public class SystemService : BaseDynamicService, ISystemService
    {
        public SystemService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

        public IEnumerable<Language> GetLanguageList()
        {
            const string SpKey = "Aff_Market_Sel";

            return FetchTData<Language>(SpKey);
        }

        public IEnumerable<Product> GetProductList()
        {
            const string SpKey = "Aff_ProductList_Sel";

            return FetchTData<Product>(SpKey);
        }

        public IEnumerable<Currency> GetBaseCurrencies()
        {
            const string SpKey = "Aff_BaseCurrency_Sel";

            return FetchTData<Currency>(SpKey);
        }

        public IEnumerable<Currency> GetCurrencies()
        {
            const string SpKey = "Aff_Currency_Sel";

            return FetchTData<Currency>(SpKey);
        }

        public IEnumerable<Currency> GetPaymentCurrencies()
        {
            const string SpKey = "Aff_PaymentCurrency_Sel";

            return FetchTData<Currency>(SpKey);
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
