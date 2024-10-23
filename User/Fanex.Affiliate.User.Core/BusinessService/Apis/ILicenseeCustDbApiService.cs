using Fanex.Affiliate.User.Core.DTO.Apis;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using System.Threading.Tasks;

namespace Fanex.Affiliate.User.Core.BusinessService.Apis
{
    public interface ILicenseeCustDbApiService
    {
        Task<LicenseeCustDbApiResult> Insert12BetPlayerAccountToIBCAsync(DepositCustomerInfo depositCustomer);
    }
}
