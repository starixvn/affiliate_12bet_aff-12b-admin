using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.ExchangeRate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.Repository.ExchangeRate
{
    public interface IExchangeRateRepository
    {
        IEnumerable<ExchangeRateInfo> GetExchangeRateList(DateTime date);

        ExchangeRateInfo GetExchangeRateDetail();

        ResultInfo UpdateExchangeRate();
    }
}
