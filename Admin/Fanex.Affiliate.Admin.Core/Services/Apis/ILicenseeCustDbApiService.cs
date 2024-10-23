using Fanex.Affiliate.Admin.Core.DTO.Apis;
using Fanex.Affiliate.Admin.Core.DTO.Member;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.Services.Apis
{
    public interface ILicenseeCustDbApiService
    {
        Task<LicenseeCustDbApiResult> Insert12BetPlayerAccountToIBCAsync(DepositCustomerInfo depositCustomer);
    }
}
