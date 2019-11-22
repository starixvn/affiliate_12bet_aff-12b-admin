using Fanex.Affiliate.DataService.Importer.Enum.GPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer
{
    public interface IGetDataByGameType<T> 
    {
        Task<IEnumerable<string>> GetRawDataByGameTypeAsync(DateTime startTime, DateTime endTime, T type);

    }
}
