using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.Utilities
{
    public interface IEbetDataSimulator
    {
        Task<string> GetLocalData(string filePath);
    }
}
