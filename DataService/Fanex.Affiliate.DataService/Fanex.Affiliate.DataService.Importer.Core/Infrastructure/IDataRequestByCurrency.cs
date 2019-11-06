using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.Infrastructure
{
    public interface IDataRequestByCurrency
    {
        IEnumerable<string> GetRawDataListByCurrencies(DateTime startTime, DateTime endTime, IEnumerable<string> currencies);

        IEnumerable<string> GetRawDataListByCurrency(DateTime startTime, DateTime endTime, string currency);
    }
}
