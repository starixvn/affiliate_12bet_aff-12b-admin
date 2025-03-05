namespace Fanex.Affiliate.User.Core.Infrastructure
{
    using Apis;
    using Constants;
    using CoreService;
    using DTO.Apis;
    using MetalModels.Apis;
    using Refit;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using Utilities;

    public class AuthenticatedHttpClientHandler : HttpClientHandler
    {
        private readonly IAppSetting _appSetting;
        private readonly ITokenApi _tokenApi;
        private readonly IMemoryCacheService _cacheService;

        public AuthenticatedHttpClientHandler(IAppSetting appSetting, IMemoryCacheService cacheService)
        {
            _appSetting = appSetting ?? throw new ArgumentNullException(nameof(cacheService));

            Proxy = appSetting.IsUseMemberSiteProxy ? new WebProxy(new Uri(appSetting.MemberSiteProxyUrl)) : null;
            AutomaticDecompression = AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            _tokenApi = TokenApiInstance(Proxy, appSetting.MemberSiteApiUrl);
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            await SetJwtToken(request);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        private async Task SetJwtToken(HttpRequestMessage request)
        {
            var token = _cacheService.Get<string>(CacheKey.TokenMemoryCache);

            if (token == null)
            {
                var tokenInfo = await GetJwtTokenAsync();
                token = tokenInfo.AccessToken;

                _cacheService.Set(CacheKey.TokenMemoryCache, token, _appSetting.MemberSiteApiTokenCacheTimeOut);
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        private async Task<TokenInfo> GetJwtTokenAsync()
        {
            var tokenRequestParam = new TokenRequestParam
            {
                Username = $"{_appSetting.MemberSiteApiUsername}",
                Password = _appSetting.MemberSiteApiPassword
            };

            try
            {
                return await _tokenApi.GetTokenAsync(tokenRequestParam);

            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex);

                throw new InvalidOperationException($"Can't get JwtSecurityToken for {tokenRequestParam.Username}!");
            }
        }

        private static ITokenApi TokenApiInstance(IWebProxy proxy, string tokenApiUrl)
        {
            var handler = new HttpClientHandler
            {
                Proxy = proxy
            };

            return RestService.For<ITokenApi>(
                new HttpClient(handler)
                {
                    BaseAddress = new Uri(tokenApiUrl)
                });
        }
    }
}