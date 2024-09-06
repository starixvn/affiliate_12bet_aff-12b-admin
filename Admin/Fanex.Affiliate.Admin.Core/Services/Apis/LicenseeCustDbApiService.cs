using Fanex.Affiliate.Admin.Core.Apis;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO.Apis;
using Fanex.Affiliate.Admin.Core.DTO.Member;
using Fanex.Affiliate.Admin.Core.MetaModels.Apis;
using Fanex.Affiliate.Admin.Core.Utilities;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.Services.Apis
{
    public class LicenseeCustDbApiService : ILicenseeCustDbApiService
    {
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

                if (!result.IsSuccessful)
                {
                    await LogHelper.WriteErrorAsync($"Insert 12Bet Player Account ({depositCustomer.DepositUserName}) To IBC is unsuccessful!" +
                        $"\n param: {JsonConvert.SerializeObject(depositCustomer)}" +
                        $"\n response: {JsonConvert.SerializeObject(result)}");
                }

                return result;
            }
            catch (Exception ex)
            {
                var errorMsg = $"Insert 12Bet Player Account ({depositCustomer.DepositUserName}) To IBC is failed!";
                LogHelper.WriteError(ex, $"{errorMsg} \n param: {JsonConvert.SerializeObject(depositCustomer)}");

                return new LicenseeCustDbApiResult
                {
                    ErrorCode = Code.GeneralErrorCode,
                    ErrorMessage = errorMsg
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
