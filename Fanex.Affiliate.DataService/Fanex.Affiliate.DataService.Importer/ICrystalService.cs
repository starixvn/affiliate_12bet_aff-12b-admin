using System.Collections.Generic;
using System.Threading.Tasks;
using Fanex.Affiliate.DataService.Importer.MetaModels.Crystal;

namespace Fanex.Affiliate.DataService.Importer
{
    public interface ICrystalService
    {
        Task<IEnumerable<string>> GetRawDataList(CrystalParam param);
    }
}
