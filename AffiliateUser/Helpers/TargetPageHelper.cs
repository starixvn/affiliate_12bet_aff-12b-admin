using System.Collections.Generic;
using System.Linq;

namespace AffiliateUser.Helpers
{
    public static class TargetPageHelper
    {
        private static readonly IEnumerable<string> _targetPageList = new List<string>
        {
            "sportsbook", "home", "casino", "signup", "promotion",
            "numbergame", "keno", "virtualsports", "playtech", "livecasino",
            "lottery", "cricket"
        };

        public static IEnumerable<string> TargetPageList
        {
            get
            {
                if (AppSettings.Session.IsCasino)
                {
                    var targetPageList = _targetPageList.ToList();
                    targetPageList.Add("12betcasino");

                    return targetPageList;
                }

                return _targetPageList;
            }
        }

        public static string GenerateDropdown(string id, string targetRequest, bool hasTextAll = false)
        {
            return Common.GenerateDropdownTextList(id, TargetPageList, targetRequest, hasTextAll);
        }
    }
}