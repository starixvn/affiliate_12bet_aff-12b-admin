using System;
using System.Linq;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;

namespace Fanex.Affiliate.Admin.Core.Services.UserPanel
{
    public class CustomerMemberService : ICustomerMemberService
    {
        private readonly ISessionService _sessionService;
        private readonly ICustomerMemberRepository _customerMemberRepository;

        public CustomerMemberService(ISessionService sessionService, ICustomerMemberRepository customerMemberRepository)
        {
            _sessionService = sessionService;
            _customerMemberRepository = customerMemberRepository;
        }

        public EntityResult<CustomerMember> GetCustomerMember(CustomerMemberParam param)
        {
            param.CanViewMemberEmail = _sessionService.UserPermissionList.Any(i => i.FunctionName.Equals(Function.MemberEmail, StringComparison.OrdinalIgnoreCase));

            return _customerMemberRepository.GetCustomerMemberList(param);
        }
    }
}