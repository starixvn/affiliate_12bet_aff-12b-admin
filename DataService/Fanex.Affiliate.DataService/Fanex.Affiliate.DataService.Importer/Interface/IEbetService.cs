using System.Collections.Generic;
using System.Threading.Tasks;
using Fanex.Affiliate.DataService.Importer.DTO.Ebet;
using Fanex.Affiliate.DataService.Importer.MetaModels.Ebet;

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
