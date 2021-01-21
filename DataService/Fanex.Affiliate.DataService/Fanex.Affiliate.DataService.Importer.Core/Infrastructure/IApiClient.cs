using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.Core.Infrastructure
{
    public interface IApiClient
    {
        Task<string> GetResponseAsync(NameValueCollection query, CancellationToken cancellationToken = default(CancellationToken));

        IHttpContentProvider ContentProvider { get; set; }

        void SetUrl(string url);
    }
}
