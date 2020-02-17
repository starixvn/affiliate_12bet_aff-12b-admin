using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.Core.Utilities
{
    public interface IDataSimulator
    {
       string GetLocalData(string filePath);
    }
}
