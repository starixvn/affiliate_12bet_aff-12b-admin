using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.DTO.System;
using Fanex.Affiliate.User.Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public static class SystemHelper
    {
        public static IEnumerable<SelectListItem> GetLanguageList(IEnumerable<Language> languageList, string preferredLanguage)
        {
            var dataList = languageList.Select(l => new SelectListItem
            {
                Value = l.MarketId,
                Text = SystemLanguage.GetLanguageText(l.MarketName),
                Selected = l.MarketId.MatchWith(preferredLanguage)
            });

            return dataList;
        }
    }
}
