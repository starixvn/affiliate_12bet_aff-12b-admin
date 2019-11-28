using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.Core.Infrastructure
{
    public interface IDataRequestByCurrency
    {
        Task<IEnumerable<string>> GetRawDataListByCurrenciesAsync(DateTime startTime, DateTime endTime, IEnumerable<string> currencies);

        Task<IEnumerable<string>> GetRawDataListByCurrencyAsync(DateTime startTime, DateTime endTime, string currency);
    }
}
