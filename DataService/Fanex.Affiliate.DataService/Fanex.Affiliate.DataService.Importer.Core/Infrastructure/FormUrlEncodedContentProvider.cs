using Fanex.Affiliate.DataService.Importer.Core.Extension;
using System.Collections.Specialized;
using System.Net.Http;

namespace Fanex.Affiliate.DataService.Importer.Core.Infrastructure
{
    public class FormUrlEncodedContentProvider : IHttpContentProvider
    {
        public HttpContent GetHttpContent(NameValueCollection param)
            => new FormUrlEncodedContent(param.ToKeyValuePairCollection());
    }
}
