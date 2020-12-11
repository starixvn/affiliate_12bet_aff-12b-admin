using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public interface IMemberRepository
    {
        IEnumerable<Member> GetMemberList(MemberParam customerParam);
    }
}
