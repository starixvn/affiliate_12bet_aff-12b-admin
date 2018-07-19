using Fanex.Affiliate.Admin.Core.DTO.Affiliate;

namespace Fanex.Affiliate.Admin.Core.Services
{
    public interface IAffiliateService
    {
        AffLeftPanelInfo GetInfo(string username);
    }
}
