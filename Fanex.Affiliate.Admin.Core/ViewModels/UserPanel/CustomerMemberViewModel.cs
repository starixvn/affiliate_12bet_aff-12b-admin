using Fanex.Affiliate.Admin.Core.DTO.Affiliate;

namespace Fanex.Affiliate.Admin.Core.ViewModels.UserPanel
{
    public class CustomerMemberViewModel
    {
        public bool CanViewMemberEmail { get; set; }
        
        public AffiliateLinkParam AffiliateLinkParam { get; set; }
    }
}
