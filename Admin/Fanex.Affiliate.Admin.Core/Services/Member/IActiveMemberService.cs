using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Member;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Member
{
    public interface IActiveMemberService
    {
        IEnumerable<ActiveMemberInfo> GetActivatedMemberList(string userNames);

        IEnumerable<UnactiveMemberInfo> GetUnactivatedMemberList(string userNames);

        ResultInfo ActiveMember(ActiveMemberParam param);
    }
}
