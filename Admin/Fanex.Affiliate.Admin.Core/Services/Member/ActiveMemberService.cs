using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Member;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;
using Fanex.Affiliate.Admin.Core.Repository.Member;
using Fanex.Affiliate.Admin.Core.Services.Apis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.Services.Member
{
    public class ActiveMemberService : IActiveMemberService
    {
        private readonly IActiveMemberRepository _activeMemberRepository;
        private readonly ILicenseeCustDbApiService _licenseeCustDbApiService;

        public ActiveMemberService(
            IActiveMemberRepository activeMemberRepository,
            ILicenseeCustDbApiService licenseeCustDbApiService)
        {
            _activeMemberRepository = activeMemberRepository;
            _licenseeCustDbApiService = licenseeCustDbApiService;
        }

        public AllMemberEntities GetAllMembers(string userNames)
        {
            return _activeMemberRepository.GetAllMembers(userNames);
        }

        public async Task<ResultInfo> ActiveMemberAsync(ActiveMemberParam param)
        {
            StringBuilder errorUserNames = new StringBuilder();
            var depositCustomerList = _activeMemberRepository.Get12BetPlayersDepositInfo(param.UserNames);

            foreach (var depositCustomer in depositCustomerList)
            {
                var apiResult = await _licenseeCustDbApiService.Insert12BetPlayerAccountToIBCAsync(depositCustomer);

                if (apiResult.IsSuccessful)
                {
                    var ibcCustInfo = apiResult.Result;

                    _activeMemberRepository.WriteLogAfterAdd12BetPlayerAccountToIBCSuccefully(param, depositCustomer.DepositUserId);

                    var result = _activeMemberRepository.Update12BetPlayerStatusToDeposit(depositCustomer.DepositUserName, ibcCustInfo.CustID, ibcCustInfo.Username);

                    if (result.IsSuccessful)
                    {
                        _activeMemberRepository.WriteLogAfterUpdate12BetPlayerStatusToDepositSuccefully(param, ibcCustInfo.CustID);
                    }
                    else
                    {
                        errorUserNames.Append("\n" + depositCustomer.DepositUserName + " " + (result.Code == Code.ErrCodeUserAlreadyHasIBCAccount ? "user already has IBC account" : "user not found"));
                    }
                }
                else
                {
                    errorUserNames.Append("\n" + depositCustomer.DepositUserName + " " + apiResult.ErrorMessage);
                }
            }

            return new ResultInfo
            {
                Code = depositCustomerList.Any() && string.IsNullOrEmpty(errorUserNames.ToString()) ? 0 : Code.GeneralErrorCode,
                Message = depositCustomerList.Any() ? errorUserNames.ToString() : "Deposit User Name(s) is incorrect"
            };
        }
    }
}
