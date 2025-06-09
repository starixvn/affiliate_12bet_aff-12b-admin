namespace Fanex.Affiliate.User.Core.Utilities
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using DTO;
    using DTO.Systems;
    using DTO.UserPanel.MediaGallery;
    using Extensions;
    using Fanex.Affiliate.User.Core.Enum;
    using Resources;

    public static class SystemHelper
    {
        public static IEnumerable<SelectListItem> GetTargetLanguageList(IEnumerable<Language> targetLanguageList, string targetLanguageCode = null)
        {
            var dataList = targetLanguageList.Select(l => new SelectListItem
            {
                Value = l.Code,
                Text = GetLanguageText(l.EnglishName),
                Selected = l.Code.MatchWith(targetLanguageCode)
            });

            return dataList;
        }

        public static IEnumerable<SelectListItem> GetDomainList(IEnumerable<Domain> domainList, AffiliateLinkType linkType, int? domainId = null)
        {
            var dataList = domainList.Select(d => new SelectListItem
            {
                Value = d.DomainId.ToString(),
                Text = linkType == AffiliateLinkType.AffiliateLink ? d.DomainDescription : d.DomainName,
                Selected = d.DomainId == domainId
            });

            return dataList;
        }

        public static IEnumerable<SelectListItem> GetPageList(IEnumerable<LandingPage> pageList)
        {
            var dataList = pageList.Select(p => new SelectListItem
            {
                Value = p.PageCode,
                Text = p.PageName
            });

            return dataList;
        }

        public static string GetLanguageText(string value)
        {
            return (value.Contains("(") 
                       ? Lang.ResourceManager.GetString(value.Replace(" (", "").Replace(")", "")) 
                       : Lang.ResourceManager.GetString(value.Replace(" ", "")))
                   ?? value;
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

        public static string GetTargetCountryText(string value)
        {
            return Lang.ResourceManager.GetString(value.Replace(" ", "")) ?? value;
        }
    }
}
