namespace Fanex.Affiliate.User.Core.Apis
{
    using System.Threading.Tasks;
    using Fanex.Affiliate.User.Core.DTO.Apis;
    using Fanex.Affiliate.User.Core.MetalModels.Apis;
    using Refit;

    public interface ITokenApi
    {
        [Post("/api/Auth")]
        Task<TokenInfo> GetTokenAsync([Body] TokenRequestParam param);
    }
}