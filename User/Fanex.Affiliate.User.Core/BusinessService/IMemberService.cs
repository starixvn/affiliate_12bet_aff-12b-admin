namespace Fanex.Affiliate.User.Core.BusinessService
{
    using System.Collections.Generic;
    using DTO.UserPanel;
    using MetalModels.UserPanel;

    public interface IMemberService
    {
        IEnumerable<Member> GetMemberList(MemberParam customerParam);
    }
}
