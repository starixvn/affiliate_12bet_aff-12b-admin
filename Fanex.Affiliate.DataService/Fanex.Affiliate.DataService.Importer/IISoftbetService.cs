using System.Collections.Generic;
using System.Threading.Tasks;
using Fanex.Affiliate.DataService.Importer.DTO.ISoftbet;
using Fanex.Affiliate.DataService.Importer.MetaModels.ISoftbet;

namespace Fanex.Affiliate.DataService.Importer
{
    public interface IISoftbetService
    {
        Task<IEnumerable<ISoftbetApiParamResultPair>> GetRawDataList(ISoftbetEntryParam param);

        string ConvertToXml(ISoftbetSummary ISoftbetSummary, ISoftbetApiParam param);

        void Import(string xmlData);

        Task Import(ISoftbetEntryParam param);
    }
}
