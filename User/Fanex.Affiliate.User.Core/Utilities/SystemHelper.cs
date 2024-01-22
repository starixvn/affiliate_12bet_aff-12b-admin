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

        public static IEnumerable<SelectListItem> GetTargetLanguageList(IEnumerable<TargetLanguage> targetLanguageList, string targetLanguageCode)
        {
            var dataList = targetLanguageList.Select(l => new SelectListItem
            {
                Value = l.LanguageCode,
                Text = SystemLanguage.GetLanguageText(l.LanguageName),
                Selected = l.LanguageCode.MatchWith(targetLanguageCode)
            });

            return dataList;
        }

        public static IEnumerable<SelectListItem> GetTargetCountryList(IEnumerable<TargetCountry> targetCountryList, string targetCountryCode)
        {
            var dataList = targetCountryList.Select(l => new SelectListItem
            {
                Value = l.TargetCountryCode,
                Text = SystemLanguage.GetLanguageText(l.TargetCountryName),
                Selected = l.TargetCountryCode.MatchWith(targetCountryCode)
            });

            return dataList;
        }
    }
}
