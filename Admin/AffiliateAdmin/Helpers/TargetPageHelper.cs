using System.Collections.Generic;
using Affiliate.Infrastructure.Helpers;

namespace AffiliateAdmin.Helpers
{
    public static class TargetPageHelper
    {
        public static string GenerateDropdown(string id, string targetRequest, bool hasAll = true)
        {
            var targetPage = new TargetPage(AppSettings.Session.IsCasino);
            var targetPageList = new Dictionary<string, string>();
            if (hasAll)
            {
                targetPageList.Add(string.Empty, "All");
            }

            foreach (var item in targetPage.TargetPageList)
            {
                targetPageList.Add(item, item);
            }

            return Common.GenerateDropdownHtml(id, targetPageList, targetRequest);
        }
    }
}