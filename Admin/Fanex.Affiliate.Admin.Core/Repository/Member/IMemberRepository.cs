using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Member;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;

namespace Fanex.Affiliate.Admin.Core.Repository.Member
{
    public interface IMemberRepository
    {
        EntityResult<MemberInfo> GetMemberList(MemberParam param);

        ResultInfo UpdateMemberStatus(UpdateMemberStatusParam param);
    }
}
