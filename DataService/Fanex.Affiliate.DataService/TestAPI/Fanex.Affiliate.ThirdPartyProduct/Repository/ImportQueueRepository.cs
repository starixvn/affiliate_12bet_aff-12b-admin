using Fanex.Affiliate.ThirdPartyProduct.DTO;
using Fanex.Affiliate.ThirdPartyProduct.MetaModels;
using Fanex.Data.SqlServer;
using System.Collections.Generic;

namespace Fanex.Affiliate.ThirdPartyProduct.Repository
{
    public class ImportQueueRepository : IImportQueueRepository
    {
        public IEnumerable<ImportQueue> GetDataList(ImportQueueParam param)
        {
            const string SpKey = "Aff_3rdProduct_ImportQueueSel";
            using (var objectDb = new SqlObjectDb(SpKey))
            {
                return objectDb.Query<ImportQueue>(param);
            }
        }

        public string GetXmlData(int queueId)
        {
            const string SpKey = "Aff_3rdProduct_XmlDataByQueueIdSel";
            using (var objectDb = new SqlObjectDb(SpKey))
            {
                return objectDb.QueryEntity<string>(new { queueId });
            }
        }
    }
}
