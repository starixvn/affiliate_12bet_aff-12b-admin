using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.Extensions;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public static class OldMarketingHelper
    {
        public static IEnumerable<SelectListItem> GetLanguageList(IEnumerable<OldBannerType> bannerTypeList, string preferredLanguage)
        {
            var languageByType = bannerTypeList.Where(t => t.Type.MatchWith(BannerTypeName.Language));

            var dataList = new List<SelectListItem>();
            foreach (var item in languageByType)
            {
                var language = SystemLanguage.LanguageList.FirstOrDefault(l => l.EnglishName.MatchWith(item.Value));
                if (language != null)
                {
                    dataList.Add(new SelectListItem
                    {
                        Value = language.Code,
                        Text = SystemLanguage.GetLanguageText(item.Value),
                        Selected = language.Code.MatchWith(preferredLanguage)
                    });
                }
            }

            return dataList;
        }
    }
}
