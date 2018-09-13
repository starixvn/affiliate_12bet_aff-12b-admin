using System.Collections.Generic;
using System.Linq;

namespace Affiliate.Infrastructure.Helpers
{
    public class TargetPage
    {
        private readonly bool _isCasino;

        public TargetPage(bool isCasino)
        {
            _isCasino = isCasino;
        }

        private static readonly IEnumerable<string> _targetPageList = new List<string>
        {
            "sportsbook", "home", "casino", "signup", "promotion",
            "numbergame", "keno", "virtualsports", "playtech", "livecasino",
            "lottery", "cricket", "esports"
        };

        public IEnumerable<string> TargetPageList
        {
            get
            {
                if (_isCasino)
                {
                    var targetPageList = _targetPageList.ToList();
                    targetPageList.Add("12betcasino");

                    return targetPageList;
                }

                return _targetPageList;
            }
        }
    }
}