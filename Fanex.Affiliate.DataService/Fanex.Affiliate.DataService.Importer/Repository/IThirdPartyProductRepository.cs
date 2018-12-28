namespace Fanex.Affiliate.DataService.Importer.Repository
{
    public interface IThirdPartyProductRepository
    {
        void ImportData(int productId, string xmlData, byte jobType = 0);
    }
}
