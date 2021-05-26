using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Member;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;

namespace Fanex.Affiliate.Admin.Core.Services.Member
{
    public interface IRegisteredMemberService
    {
        EntityResult<RegisteredMemberInfo> GetRegisteredMemberList(MemberParam param);
    }
}
