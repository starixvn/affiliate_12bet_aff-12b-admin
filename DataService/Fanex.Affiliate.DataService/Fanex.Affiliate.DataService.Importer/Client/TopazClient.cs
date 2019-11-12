using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using Fanex.Affiliate.DataService.Importer.Core.Extension;

namespace Fanex.Affiliate.DataService.Importer.Client
{
    public class TopazClient : ApiClientBase
    {
        public TopazClient() : base()
        {
        }
        public override async Task<string> GetResponseAsync(string url, NameValueCollection query)
        {
            var uri = new Uri($"{url}?{query.ToQuery()}");
            var response = await Client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            response.Dispose();
            return data;
        }
        ~TopazClient()
        {
            Dispose(false);
        }
    }
}
