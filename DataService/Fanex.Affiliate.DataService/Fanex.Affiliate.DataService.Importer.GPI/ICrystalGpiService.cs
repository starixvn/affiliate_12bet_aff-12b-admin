using System.Collections.Generic;
using Fanex.Affiliate.DataService.Importer.Gpi.MetaModels;
using Polly;

namespace Fanex.Affiliate.DataService.Importer
{
    public interface ICrystalGpiService
    {
        IEnumerable<string> GetRawDataList(CrystalGpiParam param);

        void Import(CrystalGpiParam param);

        void Import(IEnumerable<string> dataList);
    }
}
