using Fanex.Affiliate.Admin.Core.DTO.Apis;
using Fanex.Affiliate.Admin.Core.MetaModels.Apis;
using Refit;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.Apis
{
    public interface ILicenseeCustDbApi
    {
        [Post("/api/deposit/players")]
        Task<LicenseeCustDbApiResult> Insert12BetPlayerAccountToIBCAsync([Header("ApiKey")] string apiKey, [Body(BodySerializationMethod.Serialized)]LicenseeCustDbApiParam param);
    }
}
