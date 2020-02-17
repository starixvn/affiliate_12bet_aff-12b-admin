using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.Core.Infrastructure
{
    public interface IApiClient 
    {
        Task<string> GetResponseAsync(string url, NameValueCollection query);
    }
}
