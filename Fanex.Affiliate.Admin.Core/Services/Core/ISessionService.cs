using Fanex.Affiliate.Admin.Core.Enum;

namespace Fanex.Affiliate.Admin.Core.Services.Core
{
    public interface ISessionService
    {
        int SelectedAffId { get; set; }

        string SelectedAffUserName { get; set; }

        AffType SelectedAffType { get; set; }

        int CustId { get; set; }

        int OnUserId { get; set; }
    }
}
