using Fanex.Affiliate.Admin.Core.DTO.Member;
using Fanex.Affiliate.Admin.Core.Repository.Member;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;
using System;
using System.Text;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;
using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.Services.Member
{
    public class ActiveMemberService : IActiveMemberService
    {
        private readonly IActiveMemberRepository _activeMemberRepository;
        public ActiveMemberService(IActiveMemberRepository activeMemberRepository)
        {
            _activeMemberRepository = activeMemberRepository;
        }

        public IEnumerable<ActiveMemberInfo> GetActivedMemberList(string userNames)
        {
            return _activeMemberRepository.GetActivedMemberList(userNames);
        }

        public IEnumerable<UnactiveMemberInfo> GetUnactivedMemberList(string userNames)
        {
            return _activeMemberRepository.GetUnactivedMemberList(userNames);
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
                        errorUserNames.Append("\n" + depositCustomer.DepositUserName + " " + (result.Code == 102 ? "user already has IBC account" : "user not found"));
                    }
                } else
                {
                    errorUserNames.Append("\n" + depositCustomer.DepositUserName + " " + result.Message);
                }
            }

            return new ResultInfo
            {
                Code = string.IsNullOrEmpty(errorUserNames.ToString()) ? 0 : Code.GeneralErrorCode,
                Message = errorUserNames.ToString()
            };
        }
    }
}
