using Fanex.Affiliate.DataService.Importer.DTO.Ebet;
using Fanex.Affiliate.DataService.Importer.MetaModels.Ebet;

namespace Fanex.Affiliate.DataService.Importer
{
    public interface IEbetService
    {
        string GetJsonData(EbetParam param);

        EbetSummary ConvertDataFromJson(string jsonData);

        string ConvertToXml(EbetSummary summaryData, EbetParam param);

        void Import(string xmlData);
    }
}
