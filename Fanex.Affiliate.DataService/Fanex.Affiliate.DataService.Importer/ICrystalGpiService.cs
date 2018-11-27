using System.Collections.Generic;
using Fanex.Affiliate.DataService.Importer.MetaModels.CrystalGpi;

namespace Fanex.Affiliate.DataService.Importer
{
    public interface ICrystalGpiService
    {
        IEnumerable<string> GetRawDataList(CrystalGpiParam param);

        IEnumerable<string> ConvertDataTimeZone(IEnumerable<string> rawDataList);

        void Import(CrystalGpiParam param);

        void Import(IEnumerable<string> dataList);
    }
}
