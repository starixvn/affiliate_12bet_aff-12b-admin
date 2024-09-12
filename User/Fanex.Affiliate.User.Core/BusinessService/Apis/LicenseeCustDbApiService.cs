using Fanex.Affiliate.User.Core.Apis;
using Fanex.Affiliate.User.Core.DTO.Apis;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.Apis;
using Fanex.Affiliate.User.Core.Utilities;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Fanex.Affiliate.User.Core.BusinessService.Apis
{
    public class LicenseeCustDbApiService : ILicenseeCustDbApiService
    {
        private readonly int ErrorCodeException = -999;
        private readonly IAppSetting _appSetting;
        private readonly ILicenseeCustDbApi _licenseeCustDbApi;

        public LicenseeCustDbApiService(
            IAppSetting appSetting,
            ILicenseeCustDbApi licenseeCustDbApi)
        {
            _appSetting = appSetting;
            _licenseeCustDbApi = licenseeCustDbApi;
        }

        public async Task<LicenseeCustDbApiResult> Insert12BetPlayerAccountToIBCAsync(DepositCustomerInfo depositCustomer)
        {
            try
            {
                var apiParam = BuildLicenseeCustDbApiParam(depositCustomer);
                var result = await _licenseeCustDbApi.Insert12BetPlayerAccountToIBCAsync(_appSetting.LicenseeCustDbApiAuthenKey, apiParam);

                if(!result.IsSuccessful)
                {
                    await LogHelper.WriteErrorAsync($"Insert 12Bet Player Account ({depositCustomer.DepositUserName}) To IBC is unsuccessful!" +
                        $"\n param: {JsonConvert.SerializeObject(depositCustomer)}" +
                        $"\n response: {JsonConvert.SerializeObject(result)}");
                }

                return result;
            }
            catch(Exception ex)
            {
                LogHelper.WriteError(ex, $"Insert 12Bet Player Account ({depositCustomer.DepositUserName}) To IBC is failed!" +
                    $"\n param: {JsonConvert.SerializeObject(depositCustomer)}");

                return new LicenseeCustDbApiResult
                {
                    ErrorCode = ErrorCodeException
                };
            }
        }

        private static LicenseeCustDbApiParam BuildLicenseeCustDbApiParam(DepositCustomerInfo depositCustomer)
        {
            return new LicenseeCustDbApiParam
            {
                UserId = depositCustomer.DepositUserId,
                Username = depositCustomer.DepositUserName,
                Password = depositCustomer.Password,
                OddsType = depositCustomer.OddsType,
                AgentId = depositCustomer.AgentId,
                AffiliateId = depositCustomer.AffiliateId,
                Country = depositCustomer.Country
            };
        }
    }
}
