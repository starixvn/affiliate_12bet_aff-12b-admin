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
            var dataList = new List<SelectListItem>();
            foreach (var item in languageList)
            {
                var language = SystemLanguage.LanguageList.FirstOrDefault(l => l.EnglishName.MatchWith(item.MarketName));
                if (language != null)
                {
                    dataList.Add(new SelectListItem
                    {
                        Value = language.Code,
                        Text = SystemLanguage.GetLanguageText(item.MarketName),
                        Selected = language.Code.MatchWith(preferredLanguage)
                    });
                }
            }

            return dataList;
        }
    }
}
