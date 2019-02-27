using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.Utilities
{
    public interface IDataSimulator
    {
       string GetLocalData(string filePath);
    }
}
