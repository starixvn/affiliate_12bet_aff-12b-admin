using Fanex.Affiliate.DataService.Importer.DTO.Ebet;
using Fanex.Affiliate.DataService.Importer.MetaModels.Ebet;

namespace Fanex.Affiliate.DataService.Importer
{
    public interface IEbetService
    {
        string GetJsonData(EbetApiConfig apiConfig, EbetParam param);
    }
}
