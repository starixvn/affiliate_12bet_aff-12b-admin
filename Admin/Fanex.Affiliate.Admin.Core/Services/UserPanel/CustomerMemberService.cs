using Fanex.Affiliate.Admin.Core.Criterias.UserPanel;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Services.UserPanel
{
    public class CustomerMemberService : BaseDynamicService, ICustomerMemberService
    {
        public CustomerMemberService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

        public EntityResult<CustomerMember> GetCustomerMember(CustomerMemberParam param)
        {
            var command = new CustomerMemberCriteria
            {
                AffId = param.AffId,
                MemberType = param.MemberType,
                IsGetAll = param.IsGetAll,
                FromDate = param.FromDate,
                ToDate = param.ToDate,
                PageIndex = param.PageIndex,
                PageSize = param.PageSize,
            };

            return FetchEntityResult<CustomerMember>(command);
        }
    }
}