using Fanex.Affiliate.ThirdPartyProduct.DTO;
using Fanex.Affiliate.ThirdPartyProduct.MetaModels;
using System.Collections.Generic;

namespace Fanex.Affiliate.ThirdPartyProduct.Repository
{
    public interface IImportQueueRepository
    {
        IEnumerable<ImportQueue> GetDataList(ImportQueueParam param);

        string GetXmlData(int queueId);
    }
}
