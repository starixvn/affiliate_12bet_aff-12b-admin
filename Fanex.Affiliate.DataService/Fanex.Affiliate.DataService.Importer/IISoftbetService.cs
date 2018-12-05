using System.Collections.Generic;
using Fanex.Affiliate.DataService.Importer.DTO.ISoftbet;
using Fanex.Affiliate.DataService.Importer.MetaModels.ISoftbet;

namespace Fanex.Affiliate.DataService.Importer
{
    public interface IISoftbetService
    {
        IEnumerable<ISoftbetApiParamResultPair> GetRawDataList(ISoftbetEntryParam param);

        string ConvertToXml(ISoftbetSummary isoftbetSummary, ISoftbetApiParam param);

        void Import(string xmlData, byte jobType);

        void Import(ISoftbetEntryParam param);
    }
}
