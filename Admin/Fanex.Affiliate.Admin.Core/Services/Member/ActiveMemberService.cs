using Fanex.Affiliate.Admin.Core.DTO.Member;
using Fanex.Affiliate.Admin.Core.Repository.Member;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Member
{
    public class ActiveMemberService : IActiveMemberService
    {
        private readonly IActiveMemberRepository _activeMemberRepository;
        public ActiveMemberService(IActiveMemberRepository activeMemberRepository)
        {
            _activeMemberRepository = activeMemberRepository;
        }
        public IEnumerable<ActiveMemberInfo> GetActivedMemberList(string userNames)
        {
            return _activeMemberRepository.GetActivedMemberList(userNames);
        }

        public IEnumerable<UnactiveMemberInfo> GetUnactivedMemberList(string userNames)
        {
            return _activeMemberRepository.GetUnactivedMemberList(userNames);
        }
    }
}
