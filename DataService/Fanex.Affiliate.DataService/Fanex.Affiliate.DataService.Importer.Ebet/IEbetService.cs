using System.Collections.Generic;
using Fanex.Affiliate.DataService.Importer.Ebet.DTO;
using Fanex.Affiliate.DataService.Importer.Ebet.MetaModels;

namespace Fanex.Affiliate.DataService.Importer
{
    public interface IEbetService
    {
        string GetRawData(EbetParam param);
        IEnumerable<EbetApiResult> GetResultDataList(EbetParam param);

        string ConvertToXml(EbetSummary summaryData, EbetParam param);

        void Import(string xmlData);

        void Import(EbetParam param);
    }
}
