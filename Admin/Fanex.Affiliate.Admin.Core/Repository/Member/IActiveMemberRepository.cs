using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Member;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Repository.Member
{
    public interface IActiveMemberRepository
    {
        AllMemberEntities GetAllMembers(string userNames);

        IEnumerable<DepositCustomerInfo> Get12BetPlayersDepositInfo(string userNames);

        ResultInfo Add12BetPlayerAccountToIBC(DepositCustomerInfo param);

        void WriteLogAfterAdd12BetPlayerAccountToIBCSuccefully(ActiveMemberParam param, int depositUserId);

        ResultInfo Update12BetPlayerStatusToDeposit(string depositUserName, int ibcCustid, string ibcUserName);

        void WriteLogAfterUpdate12BetPlayerStatusToDepositSuccefully(ActiveMemberParam param, int ibcCustId);
    }
}
