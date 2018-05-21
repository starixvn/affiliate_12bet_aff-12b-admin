using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO.UserPanel;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface IMarketingService
    {
        IEnumerable<BannerType> GetBannerTypeList(bool isInternal);
    }
}
