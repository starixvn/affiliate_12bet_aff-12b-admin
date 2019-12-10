using Fanex.Affiliate.Admin.Core.DTO.Member;
using Fanex.Affiliate.Admin.Core.Repository.Member;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;
using System.Text;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;
using Fanex.Affiliate.Admin.Core.Constants;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Services.Member
{
    public class ActiveMemberService : IActiveMemberService
    {
        private readonly IActiveMemberRepository _activeMemberRepository;
        public ActiveMemberService(IActiveMemberRepository activeMemberRepository)
        {
            _activeMemberRepository = activeMemberRepository;
        }

        public AllMemberEntities GetAllMembers(string userNames)
        {
            return _activeMemberRepository.GetAllMembers(userNames);
        }

        public ResultInfo ActiveMember(ActiveMemberParam param)
        {
            StringBuilder errorUserNames = new StringBuilder();
            var depositCustomerList = _activeMemberRepository.Get12BetPlayersDepositInfo(param.UserNames);

            foreach (var depositCustomer in depositCustomerList)
            {
                var result = _activeMemberRepository.Add12BetPlayerAccountToIBC(depositCustomer);

                if(result.IsSuccessful)
                {
                    _activeMemberRepository.WriteLogAfterAdd12BetPlayerAccountToIBCSuccefully(param, depositCustomer.DepositUserId);

                    result = _activeMemberRepository.Update12BetPlayerStatusToDeposit(depositCustomer.DepositUserName, depositCustomer.UserId, depositCustomer.UserName);

                    if (result.IsSuccessful)
                    {
                        _activeMemberRepository.WriteLogAfterUpdate12BetPlayerStatusToDepositSuccefully(param, depositCustomer.UserId);
                    } else
                    {
                        errorUserNames.Append("\n" + depositCustomer.DepositUserName + " " + (result.Code == Code.ErrCodeUserAlreadyHasIBCAccount ? "user already has IBC account" : "user not found"));
                    }
                } else
                {
                    errorUserNames.Append("\n" + depositCustomer.DepositUserName + " " + result.Message);
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
