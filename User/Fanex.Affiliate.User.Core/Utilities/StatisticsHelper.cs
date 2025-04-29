using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using Fanex.Affiliate.User.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public static class StatisticsHelper
    {
        public static IEnumerable<LandingPage> GetFilteredLandingPageList(IAppSetting appSetting, IEnumerable<LandingPage> landingPages, string commGroupName, string language = "")
        {
            var filteredLandingPages = landingPages;
            if(appSetting.LandingPageSupportedOnMarkets.Contains(language) && DateTime.Compare(DateTimeHelper.Now, appSetting.LandingPageAvailableDateTime) > 0 )
            {
                filteredLandingPages = landingPages.Append(new LandingPage
                {
                    PageCode = "inr-12join",
                    PageName = "INR Join Landing"
                });
            }    

            if (appSetting.MarketingForCommGroups.Any(c => c.MatchWith(commGroupName)))
            {
                return filteredLandingPages;
            }

            return filteredLandingPages.Where(l => !appSetting.MarketingLandingPages.Any(m => m.MatchWith(l.PageCode)));
        }
    }
}
