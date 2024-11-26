namespace Fanex.Affiliate.User.Core.Infrastructure
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.Sockets;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;

    public class AuthenticatedHttpClientHandler : HttpClientHandler
    {
        private readonly string _machineIp;
        private readonly string _secretKey;

        public AuthenticatedHttpClientHandler(string secretKey)
        {
            _secretKey = secretKey;
            _machineIp = GetLocalIpAddress();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // See if the request has an authorize header
            request.Headers.Authorization = AuthenticationHeaderValue.Parse(Authenticate(request));

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        private string Authenticate(HttpRequestMessage request)
        {
            var requestUri = HttpUtility.UrlEncode(request.RequestUri.PathAndQuery.ToLowerInvariant().Replace("api//", "api/"));
            var requestHttpMethod = request.Method.ToString();

            //Calculate UNIX time
            var epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
            var timeSpan = DateTime.UtcNow - epochStart;
            var requestTimeStamp = Convert.ToUInt64(timeSpan.TotalSeconds).ToString();

            //create random nonce for each request
            var nonce = Guid.NewGuid().ToString("N");

            //Creating the raw signature string
            var signatureRawData = $"{_machineIp}{requestHttpMethod}{requestUri}{requestTimeStamp}{nonce}";

            var secretKeyByteArray = Convert.FromBase64String(_secretKey);

            var signature = Encoding.UTF8.GetBytes(signatureRawData);

            using (var hmac = new HMACSHA256(secretKeyByteArray))
            {
                var signatureBytes = hmac.ComputeHash(signature);
                var requestSignatureBase64String = Convert.ToBase64String(signatureBytes);

                // Setting the values in the Authorization header using custom scheme (amx)
                var authHeader = $"amx {_machineIp}:{requestSignatureBase64String}:{nonce}:{requestTimeStamp}";

                return authHeader;
            }
        }

        private static string GetLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString().Replace(":", "_");
                }
            }

            throw new InvalidOperationException("Local IP Address Not Found!");
        }
    }
}
