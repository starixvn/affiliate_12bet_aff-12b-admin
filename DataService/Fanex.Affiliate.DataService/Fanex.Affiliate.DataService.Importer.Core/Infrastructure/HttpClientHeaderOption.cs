using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Fanex.Affiliate.DataService.Importer.Core.Infrastructure
{
    public class HttpClientHeaderOption
    {
        public AuthenticationHeaderValue AuthenHeader { get; set; }

        public string AcceptContent { get; set; }

        public IEnumerable<NameValueHeaderValue> Headers { get; set; }
    }
}
