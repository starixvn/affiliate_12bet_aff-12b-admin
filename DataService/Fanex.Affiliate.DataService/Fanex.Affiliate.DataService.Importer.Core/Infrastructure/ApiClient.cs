using Fanex.Affiliate.DataService.Importer.Core.Extension;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using System;
using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.Core.Infrastructure
{
    public class ApiClient : IApiClient
    {
        protected HttpClient HttpClient => HttpClientPooling.GetHttpClient(Url);

        public string Url { get; private set; }
        public HttpMethod HttpMethod { get; private set; }
        public HttpClientHeaderOption Headers { get; private set; }
        public IHttpContentProvider ContentProvider { get; set; } = new JsonContentProvider();

        public ApiClient(string url, HttpMethod httpMethod = null, HttpClientHeaderOption headers = null)
        {
            SetUrl(url);
            HttpMethod = httpMethod ?? HttpMethod.Get;
            Headers = headers;
        }

        public virtual Task<string> GetResponseAsync(NameValueCollection query, CancellationToken cancellationToken = default(CancellationToken))
        {
            if(string.IsNullOrWhiteSpace(Url))
            {
                throw new InvalidOperationException("Url cannot be null or empty");
            }

            switch (HttpMethod.Method.ToUpper())
            {
                case "GET":
                    return GetAsync(query, cancellationToken);
                case "POST":
                    return PostAsync(query, cancellationToken);
                default:
                    throw new NotImplementedException($"Method name: {HttpMethod.Method}");
            }
        }

        public void SetUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException($"Url not is null or empty");
            }
            Url = url;
        }

        protected virtual Task<string> GetAsync(NameValueCollection query, CancellationToken cancellationToken)
        {
            var uri = new Uri($"{Url}?{query.ToQuery()}");
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return GetDataAsync(request, cancellationToken);
        }

        protected virtual Task<string> PostAsync(NameValueCollection query, CancellationToken cancellationToken)
        {
            var content = ContentProvider.GetHttpContent(query);
            var request = new HttpRequestMessage(HttpMethod.Post, Url) { Content = content };
            return GetDataAsync(request, cancellationToken);
        }

        private async Task<string> GetDataAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken = default(CancellationToken))
        {
            SetUpRequestHeaders(requestMessage, Headers);
            try
            {
                using (var response = await HttpClient.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();
                    var bytesData = await response.Content.ReadAsByteArrayAsync();
                    var data = string.Empty;
                    if (response.Content.Headers.ContentEncoding.Any(c => string.Equals(c, "gzip", StringComparison.OrdinalIgnoreCase)))
                    {
                        var decodeBytes = Decompress(bytesData);
                        data = Encoding.UTF8.GetString(decodeBytes, 0, decodeBytes.Length);
                    }
                    else
                    {
                        data = Encoding.UTF8.GetString(bytesData);
                    }

                    return data;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex);
                return $"Error message: {ex.Message}";
            }
        }

        protected virtual void SetUpRequestHeaders(HttpRequestMessage request, HttpClientHeaderOption headers)
        {
            if (headers == null)
            {
                return;
            }

            var defaultHeaders = request.Headers;
            defaultHeaders.Authorization = headers.AuthenHeader ?? defaultHeaders.Authorization;
            if (!string.IsNullOrWhiteSpace(headers.AcceptContent))
            {
                defaultHeaders.Accept.Clear();
                defaultHeaders.Accept.ParseAdd(headers.AcceptContent);
            }
            foreach (var header in headers.Headers)
            {
                defaultHeaders.Add(header.Name, header.Value);
            }
        }

        protected static byte[] Decompress(byte[] gzip)
        {
            using (GZipStream stream = new GZipStream(new MemoryStream(gzip), CompressionMode.Decompress))
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    stream.CopyTo(memory);
                    return memory.ToArray();
                }
            }
        }
    }
}
