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
        public static IEnumerable<SelectListItem> GetTargetLanguageList(IEnumerable<TargetLanguage> targetLanguageList, string targetLanguageCode = null)
        {
            var dataList = targetLanguageList.Select(l => new SelectListItem
            {
                Value = l.LanguageCode,
                Text = SystemLanguage.GetLanguageText(l.LanguageName),
                Selected = l.LanguageCode.MatchWith(targetLanguageCode)
            });

            return dataList;
        }

        public static IEnumerable<SelectListItem> GetTargetCountryList(IEnumerable<TargetCountry> targetCountryList, string targetCountryCode = null)
        {
            var dataList = targetCountryList.Select(l => new SelectListItem
            {
                Value = l.TargetCountryCode,
                Text = l.TargetCountryName,
                Selected = l.TargetCountryCode.MatchWith(targetCountryCode)
            });

            return dataList;
        }
    }
}
