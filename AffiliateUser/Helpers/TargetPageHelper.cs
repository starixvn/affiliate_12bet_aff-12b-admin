using System.Collections.Generic;
using Affiliate.Infrastructure.Helpers;

namespace AffiliateUser.Helpers
{
    public static class TargetPageHelper
    {
        public static IEnumerable<string> TargetPageList
        {
            get
            {
                var targetPage = new TargetPage(AppSettings.Session.IsCasino);

                return targetPage.TargetPageList;
            }
        }

        public static string GenerateDropdown(string id, string targetRequest, bool hasTextAll = false)
        {
            var targetPage = new TargetPage(AppSettings.Session.IsCasino);

            return Common.GenerateDropdownTextList(id, targetPage.TargetPageList, targetRequest, hasTextAll);
        }
    }
}