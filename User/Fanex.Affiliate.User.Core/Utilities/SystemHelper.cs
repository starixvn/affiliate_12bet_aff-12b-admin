using Fanex.Affiliate.User.Core.DTO.System;
using Fanex.Affiliate.User.Core.Extensions;
using Fanex.Affiliate.User.Core.Resources;
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
                Text = GetLanguageText(l.LanguageName),
                Selected = l.LanguageCode.MatchWith(targetLanguageCode)
            });

            return dataList;
        }

        public static IEnumerable<SelectListItem> GetTargetCountryList(IEnumerable<TargetCountry> targetCountryList, string targetCountryCode = null)
        {
            var dataList = targetCountryList.Select(l => new SelectListItem
            {
                Value = l.TargetCountryCode,
                Text = GetTargetCountryText(l.TargetCountryName),
                Selected = l.TargetCountryCode.MatchWith(targetCountryCode)
            });

            return dataList;
        }

        public static string GetLanguageText(string value)
        {
            return (value.Contains("(") ? Lang.ResourceManager.GetString(value.Replace(" (", "").Replace(")", ""))
                  : Lang.ResourceManager.GetString(value.Replace(" ", ""))) ?? value;
        }

        public static string GetProductText(string value)
        {
            return (value.Contains("-") ? Lang.ResourceManager.GetString(value.Replace("-", "")) : Lang.ResourceManager.GetString(value.Replace(" ", ""))) ?? value;
        }

        public static string GetTargetCountryText(string value)
        {
            return Lang.ResourceManager.GetString(value.Replace(" ", "")) ?? value;
        }
    }
}
