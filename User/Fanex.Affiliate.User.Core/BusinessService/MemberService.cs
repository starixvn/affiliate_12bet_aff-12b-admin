namespace Fanex.Affiliate.User.Core.BusinessService
{
    using System.Collections.Generic;
    using CoreService;
    using Criterias;
    using Data.Repository;
    using DTO.UserPanel;
    using MetalModels.UserPanel;
    using Utilities;

    public class MemberService : BaseDynamicService, IMemberService
    {
        public MemberService(IDynamicRepository dynamicRepository) : base(dynamicRepository)
        {
        }

        public IEnumerable<Member> GetMemberList(MemberParam customerParam)
        {
            var criteria = customerParam.MapTo<GetMemberListCriteria>();

            return FetchTDataWithPaging<Member>(criteria, customerParam);
        }
    }
}
