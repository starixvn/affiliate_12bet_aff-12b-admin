using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.Core.Repository
{
    public interface IThirdPartyProductRepository
    {
        void ImportData(int productId, string xmlData, byte jobType = 0);
        void ImportData(int productId, IEnumerable<string> xmlDatas, byte jobType = 0);
        Task ImportDataAsync(int productId, string xmlData, byte jobType = 0);
        Task ImportDataAsync(int productId, IEnumerable<string> xmlDatas, byte jobType = 0);
    }
}
