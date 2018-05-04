using System.Linq;
using Affiliate.Infrastructure.Helpers;

namespace AffiliateAdmin.Helpers
{
    public static class TargetPageHelper
    {
        public static string GenerateDropdown(string id, string targetRequest, bool hasAll = true)
        {
            var targetPage = new TargetPage(AppSettings.Session.IsCasino);
            var targetPageList = targetPage.TargetPageList.ToList();

            if (hasAll)
            {
                targetPageList.Insert(0, Resources.Lang.All);
            }

            return Common.GenerateDropdownHtml(id, targetPageList.ToDictionary(p => p), targetRequest);
        }
    }
}