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

        public ResultInfo DeleteMember(DeleteMemberParam param)
        {
            var result = _memberRepository.DeleteMember(param);

            if(result.IsSuccessful)
            {
                result = _memberRepository.DeleteMemberInDepositDB(param.DepositMemberId);

                if(result.IsSuccessful)
                {
                    _memberRepository.WriteAfterDeleteSuccefully(param);
                }
            }

            return result;
        }
    }
}
