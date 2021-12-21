using System.Threading.Tasks;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public interface IHttpClientUtility
    {
        Task<string> GetContent(string path, string fileName);
    }
}
