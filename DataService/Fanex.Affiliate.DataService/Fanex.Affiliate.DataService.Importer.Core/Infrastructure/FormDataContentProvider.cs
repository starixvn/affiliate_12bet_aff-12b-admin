using Fanex.Affiliate.DataService.Importer.Core.Extension;
using System.Collections.Specialized;
using System.Net.Http;

namespace Fanex.Affiliate.DataService.Importer.Core.Infrastructure
{
    public class FormDataContentProvider : IHttpContentProvider
    {
        public HttpContent GetHttpContent(NameValueCollection param)
        {
            var dic = param.ToDictionary();
            var content = new FormUrlEncodedContent(dic);
            var formData = new MultipartFormDataContent
            {
                content
            };

            return formData;
        }
    }
}
