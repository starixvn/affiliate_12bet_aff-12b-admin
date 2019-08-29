using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.ExchangeRate;
using Fanex.Affiliate.Admin.Core.Repository.ExchangeRate;

namespace Fanex.Affiliate.Admin.Core.Services.ExchangeRate
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly IExchangeRateRepository _exchangeRateRepository;
        public ExchangeRateService(IExchangeRateRepository exchangeRateRepository)
        {
            _exchangeRateRepository = exchangeRateRepository;
        }

        public IEnumerable<ExchangeRateInfo> GetExchangeRateList(DateTime date)
        {
            return _exchangeRateRepository.GetExchangeRateList(date);
        }

        public ResultInfo UpdateExchangeRate()
        {
            throw new NotImplementedException();
        }
    }
}
