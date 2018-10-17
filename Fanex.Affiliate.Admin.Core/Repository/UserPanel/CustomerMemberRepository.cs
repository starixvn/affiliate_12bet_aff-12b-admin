using System;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;

namespace Fanex.Affiliate.Admin.Core.Repository.UserPanel
{
    public class CustomerMemberRepository : BaseRepository, ICustomerMemberRepository
    {
        public EntityResult<CustomerMember> GetCustomerMemberList(CustomerMemberParam param)
        {
            throw new NotImplementedException();
        }
    }
}
