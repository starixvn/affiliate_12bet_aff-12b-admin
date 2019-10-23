using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Member;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Member
{
    public interface IActiveMemberService
    {
        IEnumerable<ActiveMemberInfo> GetActivedMemberList(string userNames);

        IEnumerable<UnactiveMemberInfo> GetUnactivedMemberList(string userNames);

        ResultInfo ActiveMember(ActiveMemberParam param);
    }
}
