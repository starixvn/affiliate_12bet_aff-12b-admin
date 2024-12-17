using Fanex.Affiliate.Admin.Core.Commands.Member;
using Fanex.Affiliate.Admin.Core.Criterias.Member;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Member;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;
using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Services.Member
{
    public class MemberService : BaseDynamicService, IMemberService
    {
        public MemberService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

        public EntityResult<MemberInfo> GetMemberList(MemberParam param)
        {
            var criteria = new MemberCriteria
            {
                AffUsername = param.AffName,
                UserName = param.UserName,
                IBCUserName = param.IbcUserName,
                PageIndex = param.PageIndex,
                PageSize = param.PageSize,
            };

            return FetchEntityResult<MemberInfo>(criteria);
        }

        public PlayerActionInfo GetMemberDetail(int memberId)
        {
            var criteria = new MemberDetailCriteria
            {
                MemberId = memberId,
            };

            return GetTData<PlayerActionInfo>(criteria);
        }

        public ResultInfo UpdateMember(UpdateMemberParam param)
        {
            var command = new UpdateMemberCommand
            {
                MemberId = param.MemberId,
                IsInclude = param.IsInclude,
                IsRemoved = param.IsRemoved,
            };

            return ExecuteReturnResultInfo(command, param);
        }
    }
}
