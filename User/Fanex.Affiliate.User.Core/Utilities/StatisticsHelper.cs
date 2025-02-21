using System.Collections.Generic;
using System.Linq;
using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using Fanex.Affiliate.User.Core.Extensions;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public static class StatisticsHelper
    {
        public static IEnumerable<LandingPage> GetFilteredLandingPageList(IAppSetting appSetting, IEnumerable<LandingPage> landingPages, string commGroupName)
        {
            if (appSetting.MarketingForCommGroups.Any(c => c.MatchWith(commGroupName)))
            {
                return landingPages;
            }

            return landingPages.Where(l => !appSetting.MarketingLandingPages.Any(m => m.MatchWith(l.PageCode)));
        }
    }
}
