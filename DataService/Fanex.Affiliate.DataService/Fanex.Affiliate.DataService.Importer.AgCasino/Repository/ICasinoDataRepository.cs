namespace Fanex.Affiliate.DataService.Importer.Repository
{
    using AgCasino.DTO;
    using Fanex.Affiliate.DataService.Importer.Core.DTO;

    public interface ICasinoDataRepository
    {
        ResultInfo ImportDataFor12Bet(CasinoDataMetaModel metaModel);

        ResultInfo ImportDataForIncomeAccess(CasinoDataMetaModel metaModel);
    }
}