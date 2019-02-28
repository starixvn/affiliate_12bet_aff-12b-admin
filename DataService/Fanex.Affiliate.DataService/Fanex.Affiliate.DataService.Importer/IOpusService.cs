using System.Collections.Generic;
using Fanex.Affiliate.DataService.Importer.MetaModels.Opus;

namespace Fanex.Affiliate.DataService.Importer
{
    public interface IOpusService
    {
        IEnumerable<string> GetRawDataList(OpusParam param);

        IEnumerable<string> ConvertDataTimeZone(IEnumerable<string> rawDataList);

        void Import(string xmlData);

        void Import(OpusParam param);
    }
}
