using Fanex.Affiliate.DataService.Importer.Core.Extension;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using System;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.KY.Client
{
    public class KYGamingClient : ApiClientBase
    {
        public override async Task<string> GetResponseAsync(string url, NameValueCollection query)
        {
            var client = HttpClientPooling.GetHttpClient(url);
            var uri = new Uri($"{url}?{query.ToQuery()}");
            using (var response = await client.GetAsync(uri).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var byteResult = await response.Content.ReadAsByteArrayAsync();
                return Encoding.UTF8.GetString(byteResult);
            }
        }
    }
}
