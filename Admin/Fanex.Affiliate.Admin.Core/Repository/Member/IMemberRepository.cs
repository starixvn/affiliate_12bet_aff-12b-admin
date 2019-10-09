using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Member;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;

namespace Fanex.Affiliate.Admin.Core.Repository.Member
{
    public interface IMemberRepository
    {
        EntityResult<MemberInfo> GetMemberList(MemberParam param);

        ResultInfo DeleteMember(DeleteMemberParam param);

        ResultInfo DeleteMemberInDepositDB(DeleteMemberParam param);

        void WriteAfterDeleteSuccefully(DeleteMemberParam param);
    }
}
