
namespace Fanex.Affiliate.DataService.Importer.KYGaming
{
    using MetaModels;
    using System.Collections.Generic;

    public interface IKYService
    {
        IEnumerable<string> GetRawDataList(KYApiParam param);

        void Import(string xmlData);

        void Import(KYApiParam param);
    }
}
