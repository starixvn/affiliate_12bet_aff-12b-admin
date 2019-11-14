using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.Core.Infrastructure
{
    public abstract class ApiClientBase : IApiClient
    {
        protected readonly Lazy<HttpClient> _lazyClient = new Lazy<HttpClient>(() => new HttpClient(), true);
        protected HttpClient Client => _lazyClient.Value;
        protected bool disposed = false;
        protected ApiClientBase(HttpClient client = null)
        {
            _lazyClient = client == null ? _lazyClient : new Lazy<HttpClient>(() => client, true);
        }

        public string GetResponse(string url, NameValueCollection query)
        {
            var result = GetResponseAsync(url, query).ConfigureAwait(false).GetAwaiter().GetResult();

            return result;
        }

        public abstract Task<string> GetResponseAsync(string url, NameValueCollection query);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (_lazyClient.IsValueCreated)
                Client.Dispose();
            disposed = true;
        }

        ~ApiClientBase()
        {
            Dispose(false);
        }
    }
}
