using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.ExchangeRate;
using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.ExchangeRate
{
    public interface IExchangeRateService
    {
        IEnumerable<ExchangeRateInfo> GetExchangeRateList(DateTime date);

        ResultInfo UpdateExchangeRate();
    }
}
