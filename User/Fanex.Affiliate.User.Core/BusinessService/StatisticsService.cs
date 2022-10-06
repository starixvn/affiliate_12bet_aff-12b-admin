using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
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
        private readonly ILanguageService _languageService;
        public StatisticsService(
            IStatisticsRepository statisticsRepository,
            ILanguageService languageService)
        {
            _statisticsRepository = statisticsRepository;
            _languageService = languageService;
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
                var languageList = _languageService.GetSystemLanguages();
                landingPageStatisticsParam.LanguageCodes = GetShortedCode(languageList, landingPageStatisticsParam.LanguageCode);
                landingPageStatisticsParam.CountryCodes = string.Empty;
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

        public IEnumerable<string> GetTrackingCode(int affId, bool isInternal)
        {
            return _statisticsRepository.GetTrackingCode(affId, isInternal);
        }

        public IEnumerable<TrackingStatistics> GetTrackingStatistics(int affId, string trackingCode, bool isInternal)
        {
            return _statisticsRepository.GetTrackingStatistics(affId, trackingCode, isInternal);
        }

        public IEnumerable<TrackingStatistics> GetTrackingStatisticsDetail(TrackingStatisticsDetailParam trackingStatisticsDetailParam)
        {
            return _statisticsRepository.GetTrackingStatisticsDetail(trackingStatisticsDetailParam);
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
