using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;

namespace Fanex.Affiliate.Admin.Core.ViewModels.UserPanel
{
    public class CustomerMemberViewModel
    {
        public IEnumerable<CustomerMember> MainData { get; set; }

        public int TotalItem { get; set; }

        public bool HasHostPermission { get; set; }
    }
}
