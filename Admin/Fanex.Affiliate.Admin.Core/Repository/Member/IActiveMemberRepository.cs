using Fanex.Affiliate.Admin.Core.DTO.Member;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Repository.Member
{
    public interface IActiveMemberRepository
    {
        IEnumerable<ActiveMemberInfo> GetActivedMemberList(string userNames);

        IEnumerable<UnactiveMemberInfo> GetUnactivedMemberList(string userNames);
    }
}
