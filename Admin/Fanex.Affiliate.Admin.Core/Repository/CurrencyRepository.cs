using Fanex.Affiliate.Admin.Core.Criterias;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Data.Repository;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly IDynamicRepository _dynamicRepository;

        public CurrencyRepository(
            IDynamicRepository dynamicRepository)
        {
            _dynamicRepository = dynamicRepository;
        }

        public IEnumerable<Currency> GetBaseCurrencies()
        {
            const string SpKey = "Aff_BaseCurrency_Sel";

            return _dynamicRepository.Fetch<Currency>(new BaseCriteria(SpKey));
        }

        public IEnumerable<Currency> GetCurrencies()
        {
            const string SpKey = "Aff_Currency_Sel";

            return _dynamicRepository.Fetch<Currency>(new BaseCriteria(SpKey));
        }

        public IEnumerable<Currency> GetPaymentCurrencies()
        {
            const string SpKey = "Aff_PaymentCurrency_Sel";

            return _dynamicRepository.Fetch<Currency>(new BaseCriteria(SpKey));
        }
    }
}
