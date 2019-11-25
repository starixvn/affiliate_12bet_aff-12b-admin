using System.Collections.Generic;
using System.Threading.Tasks;
using Fanex.Affiliate.DataService.Importer.Betsoft.MetaModels;

namespace Fanex.Affiliate.DataService.Importer
{
    public interface IBetsoftService
    {
        void Import(string xmlData);

        void Import(BetsoftParam param);

        Task<string> GetRawDataAsync(BetsoftParam param);

        Task<IList<string>> GetMultipleRawDataBatchAsync(BetsoftParam param);
    }
}