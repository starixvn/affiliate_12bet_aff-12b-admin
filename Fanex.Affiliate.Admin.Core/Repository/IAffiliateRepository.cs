using Fanex.Affiliate.Admin.Core.DTO.Affiliate;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public interface IAffiliateRepository
    {
        AffLeftPanelInfo GetInfo(string username);
    }
}
