using System.Collections.Generic;
using Fanex.Affiliate.DataService.Importer.MetaModels.Crystal;

namespace Fanex.Affiliate.DataService.Importer
{
    public interface ICrystalService
    {
        IEnumerable<string> GetRawDataList(CrystalParam param);

        IEnumerable<string> ConvertDataTimeZone(IEnumerable<string> rawDataList);
    }
}
