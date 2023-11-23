using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.System;
using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using Fanex.Affiliate.User.Core.DTO.UserPanel.Statistics;
using Fanex.Affiliate.User.Core.Extensions;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IStatisticsRepository _statisticsRepository;
        private readonly ISystemService _systemService;
        private readonly IMemoryCacheService _cacheService;
        private readonly IAppSetting _appSetting;

        public StatisticsService(
            IStatisticsRepository statisticsRepository,
            ISystemService systemService,
            IMemoryCacheService cacheService,
            IAppSetting appSetting)
        {
            _statisticsRepository = statisticsRepository;
            _systemService = systemService;
            _cacheService = cacheService;
            _appSetting = appSetting;
        }

        public IEnumerable<LandingPage> GetLandingPageList(string commGroupName, bool isInternal)
        {
            var landingPages = _cacheService.Get<IEnumerable<LandingPage>>(CacheKey.LandingPageList);

            if (landingPages == null)
            {
                landingPages = _statisticsRepository.GetLandingPageList(isInternal);

                var filteredLandingPageList = StatisticsHelper.GetFilteredLandingPageList(_appSetting, landingPages, commGroupName);

                _cacheService.Set(CacheKey.LandingPageList, filteredLandingPageList);
            }

            return landingPages ?? new List<LandingPage>();
        }

        public IEnumerable<SelectListItem> GetCountryList(bool isInternal)
        {
            var countryList = _statisticsRepository.GetCountryList(isInternal);

            return StatisticsHelper.GetCountryList(countryList);
        }

        public string GetCountryCodes(bool isInternal)
        {
            var countryList = _statisticsRepository.GetCountryList(isInternal);

            return string.Join(",", countryList.Select(c => c.CountryCode));
        }

        public IEnumerable<LandingPageStatistics> GetLandingPageStatistics(LandingPageStatisticsParam landingPageStatisticsParam)
        {
            if (!string.Equals(Statistics.ReportByCountry, landingPageStatisticsParam.ReportBy, StringComparison.InvariantCultureIgnoreCase))
            {
                var languageList = _systemService.GetSystemLanguages();
                landingPageStatisticsParam.LanguageCodes = GetShortedCode(languageList, landingPageStatisticsParam.LanguageCode);
                landingPageStatisticsParam.CountryCodes = null;

                var dataList = _statisticsRepository.GetLandingPageStatistics(landingPageStatisticsParam);

                foreach (var item in dataList)
                {
                    var language = languageList.FirstOrDefault(p => p.ShortedCode == item.LanguageCode);

                    item.Language = language == null ? item.LanguageCode : language.MarketNativeName;
                }

                return dataList;
            }

            return _statisticsRepository.GetLandingPageStatistics(landingPageStatisticsParam);
        }

        public IEnumerable<ReferrerStatistics> GetReferrerStatistics(ReferrerStatisticsParam referrerStatisticsParam)
        {
            return _statisticsRepository.GetReferrerStatistics(referrerStatisticsParam);
        }

        public IEnumerable<ReferrerStatistics> GetStatisticsDetailBySite(StatisticsDetailBySiteParam statisticsDetailParam)
        {
            return _statisticsRepository.GetStatisticsDetailBySite(statisticsDetailParam);
        }

        private static string GetShortedCode(IEnumerable<Language> languageList, string languageCode)
        {
            if (string.IsNullOrEmpty(languageCode))
            {
                var allShortedCode = languageList.Select(l => l.ShortedCode).ToList();

                return string.Join(",", allShortedCode);
            }

            var shortedCode = languageList.FirstOrDefault(l => l.MarketId.MatchWith(languageCode))?.ShortedCode ?? SystemLanguage.ShortedDefaultCode;

            return shortedCode;
        }
    }
}
