using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Member;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;

namespace Fanex.Affiliate.Admin.Core.Services.Member
{
    public interface IActiveMemberService
    {
        AllMemberEntities GetAllMembers(string userNames);

        ResultInfo ActiveMember(ActiveMemberParam param);
    }
}
