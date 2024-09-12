using Fanex.Affiliate.User.Core.DTO.Apis;
using Fanex.Affiliate.User.Core.MetalModels.Apis;
using Refit;
using System.Threading.Tasks;

namespace Fanex.Affiliate.User.Core.Apis
{
    public interface ILicenseeCustDbApi
    {
        [Post("/api/deposit/players")]
        Task<LicenseeCustDbApiResult> Insert12BetPlayerAccountToIBCAsync([Header("ApiKey")] string apiKey, [Body(BodySerializationMethod.Serialized)] LicenseeCustDbApiParam param);
    }
}
