using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using Fanex.Affiliate.User.Core.DTO.UserPanel.Statistics;
using Fanex.Affiliate.User.Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

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

        public static IEnumerable<SelectListItem> GetCountryList(IEnumerable<Country> countryList)
        {
            var dataList = countryList.Select(c =>
            {
                var countryName = ToCapitalize(c.CountryName);

                return new SelectListItem
                {
                    Value = c.CountryCode,
                    Text = countryName,
                    Selected = true
                };
            });

            return dataList;
        }

        private static string ToCapitalize(string orgString)
        {
            var items = orgString.Split(' ');
            var capitalizeString = new StringBuilder();

            foreach (var item in items)
            {
                var lowerStr = item.ToLowerInvariant();
                capitalizeString.Append(char.ToUpper(item[0]) + (lowerStr.Length > 1 ? lowerStr.Substring(1) : string.Empty) + " ");
            }
            return capitalizeString.ToString();
        }
    }
}
