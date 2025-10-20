using Fanex.Affiliate.Admin.Core.DTO.Api;
using Fanex.Affiliate.Admin.Core.MetaModels.Api;
using Refit;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.Apis
{
    internal interface ITokenApi
    {
        [Post("/api/Auth")]
        Task<TokenInfo> GetTokenAsync([Body] TokenRequestParam param);
    }
}
