using System.Collections.Specialized;
using System.Net.Http;

namespace Fanex.Affiliate.DataService.Importer.Core.Infrastructure
{
    public interface IHttpContentProvider
    {
        HttpContent GetHttpContent(NameValueCollection param);
    }
}
