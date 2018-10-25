using System.Collections.Generic;
using Fanex.Affiliate.DataService.Importer.DTO.Betsoft;
using Fanex.Affiliate.DataService.Importer.MetaModels.Betsoft;

namespace Fanex.Affiliate.DataService.Importer
{
    public interface IBetsoftService
    {
        string GetData(BetsoftParam param);

        IList<BetsoftDataItem> ConvertToDataList(string xmlData);

        void Import(IList<BetsoftDataItem> dataList);
    }
}