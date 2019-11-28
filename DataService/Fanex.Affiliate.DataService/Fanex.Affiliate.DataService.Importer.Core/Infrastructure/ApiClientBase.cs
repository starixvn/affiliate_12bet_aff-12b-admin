using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.Core.Infrastructure
{
    public abstract class ApiClientBase : IApiClient
    {
        public string GetResponse(string url, NameValueCollection query)
        {
            var result = Task.Run(() => GetResponseAsync(url, query)).GetAwaiter().GetResult();
            return result;
        }

        public abstract Task<string> GetResponseAsync(string url, NameValueCollection query);
   
    }
}
