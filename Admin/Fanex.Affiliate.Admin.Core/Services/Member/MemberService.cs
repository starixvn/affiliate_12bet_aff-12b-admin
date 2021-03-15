using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Member;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;
using Fanex.Affiliate.Admin.Core.Repository.Member;

namespace Fanex.Affiliate.Admin.Core.Services.Member
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public EntityResult<MemberInfo> GetMemberList(MemberParam param)
        {
            return _memberRepository.GetMemberList(param);
        }

        public ResultInfo UpdateMemberStatus(UpdateMemberStatusParam param)
        {
            return _memberRepository.UpdateMemberStatus(param);
        }
    }
}
