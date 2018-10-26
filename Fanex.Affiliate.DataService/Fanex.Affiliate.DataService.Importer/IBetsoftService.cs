using System.Collections.Generic;
using Fanex.Affiliate.DataService.Importer.DTO.Betsoft;
using Fanex.Affiliate.DataService.Importer.MetaModels.Betsoft;

namespace Fanex.Affiliate.DataService.Importer
{
    public interface IBetsoftService
    {
        string GetRawData(BetsoftParam param);

        IList<string> GetMultipleRawDataBatch(BetsoftParam param);

        IList<BetsoftDataItem> ConvertToDataList(string rawData);

        void Import(IList<BetsoftDataItem> dataList);
    }
}