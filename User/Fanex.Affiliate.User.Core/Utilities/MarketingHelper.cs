using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.Extensions;
using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public static class MarketingHelper
    {
        public static IEnumerable<SelectListItem> GetLanguageList(IEnumerable<LanguageType> languageList, string preferredLanguage)
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
