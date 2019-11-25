using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.Core.Infrastructure
{
    public interface IGetDataByGameType<T> 
    {
        Task<IEnumerable<string>> GetRawDataByGameTypeAsync(DateTime startTime, DateTime endTime, T type);

    }
}
