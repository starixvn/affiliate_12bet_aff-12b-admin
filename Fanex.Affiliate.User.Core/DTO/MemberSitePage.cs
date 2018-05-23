using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.User.Core.DTO
{
    public class MemberSitePage
    {
        private readonly bool _isCasino;

        public MemberSitePage(bool isCasino)
        {
            _isCasino = isCasino;
        }

        private static readonly IEnumerable<string> _pageList = new List<string>
        {
            "sportsbook", "home", "casino", "signup", "promotion",
            "numbergame", "keno", "virtualsports", "playtech", "livecasino",
            "lottery", "cricket"
        };

        public IEnumerable<string> PageList
        {
            get
            {
                if (_isCasino)
                {
                    var targetPageList = _pageList.ToList();
                    targetPageList.Add("12betcasino");

                    return targetPageList;
                }

                return _pageList;
            }
        }
    }
}