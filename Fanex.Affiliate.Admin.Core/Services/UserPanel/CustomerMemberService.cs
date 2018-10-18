using System;
using System.Linq;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.ViewModels.UserPanel;

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

        public CustomerMemberViewModel GetCustomerMember(CustomerMemberParam param)
        {
            var hasHostPerimission = _sessionService.UserPermissionList.Any(i =>
            i.FunctionName.Equals(Function.MemberEmail, StringComparison.OrdinalIgnoreCase) && i.IsHost);

            param.AffId = _sessionService.AffId;
            param.HasHostPermission = hasHostPerimission;
            var entityResult = _customerMemberRepository.GetCustomerMemberList(param);


            return new CustomerMemberViewModel
            {
                MainData = entityResult.MainData,
                TotalItem = entityResult.TotalItem,
                HasHostPermission = hasHostPerimission
            };
        }
    }
}