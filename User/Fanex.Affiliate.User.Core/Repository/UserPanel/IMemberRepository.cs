using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public interface IMemberRepository
    {
        IEnumerable<Member> GetMemberList(MemberParam customerParam);
    }
}
