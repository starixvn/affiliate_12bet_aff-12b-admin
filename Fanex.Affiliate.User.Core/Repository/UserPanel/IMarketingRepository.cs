using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO.UserPanel;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public interface IMarketingRepository
    {
        IEnumerable<BannerType> GetBannerTypeList(bool isInternal);
    }
}
