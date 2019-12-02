using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.Core.Infrastructure
{
    public abstract class ApiClientBase : IApiClient
    {
        public abstract Task<string> GetResponseAsync(string url, NameValueCollection query);   
    }
}
