namespace Fanex.Affiliate.Link.Services.Impl
{
    using Fanex.Affiliate.Link.Commands;
    using Fanex.Affiliate.Link.Constants;
    using Fanex.Affiliate.Link.Core.Models;
    using Fanex.Affiliate.Link.Criterias;
    using Fanex.Affiliate.Link.DTOs;
    using Fanex.Affiliate.Link.Models;
    using Fanex.Affiliate.Link.Services.Interface;
    using Fanex.Data.Repository;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ClickTrackingEventService : IClickTrackingEventService
    {
        private readonly IDynamicRepository _dynamicRepository;
        private readonly IMemoryCacheService _memoryCacheService;

        public ClickTrackingEventService()
            : this(null, null)
        {
        }

        private ClickTrackingEventService(IDynamicRepository dynamicRepository,
            IMemoryCacheService memoryCacheService)
        {
            _dynamicRepository = dynamicRepository ?? new DynamicRepository();
            _memoryCacheService = memoryCacheService ?? new MemoryCacheService();
        }

        public string InsertStatistic(TrackingSystemCommand command)
        {
            _dynamicRepository.Execute(command);

            return command.RedirectPage;
        }

        public CheckMarketingLinkResult GetTrackingMarketingLinkInfo(CheckMarketingLinkCommand command)
        {
            var cacheKey = $"{CacheKey.MEDIA_LINK_CACHE_KEY}_{command.AffMediaLink}";
            var checkMarketingLinkInfo = _memoryCacheService.Get<CheckMarketingLinkResult>(cacheKey);
            if (checkMarketingLinkInfo is null || (checkMarketingLinkInfo.IsValidDomain && !checkMarketingLinkInfo.IsExistedTracking))
            {
                _dynamicRepository.Execute(command);
                checkMarketingLinkInfo = new CheckMarketingLinkResult
                {
                    IsValidDomain = command.IsValidDomain,
                    IsExistedTracking = command.IsExistedTracking,
                };

                _memoryCacheService.Set(cacheKey, checkMarketingLinkInfo);
            }

            return checkMarketingLinkInfo;
        }

        public bool ToggleMarketingLink(ToggleMarketingLinkCommand command)
        {
            _dynamicRepository.Execute(command);

            return command.ErrCode == 0;
        }

        public IEnumerable<MarketInfo> GetMarkets()
        {
            var markets = _memoryCacheService.Get<IEnumerable<MarketInfo>>(CacheKey.SUPPORTED_MARKET_CACHE_KEY);
            if (markets is null)
            {
                var criteria = new BaseCriteria("Aff_Market_Sel");
                markets = _dynamicRepository.Fetch<MarketInfo>(criteria);

                _memoryCacheService.Set(CacheKey.SUPPORTED_MARKET_CACHE_KEY, markets);
            }

            return markets ?? new List<MarketInfo>();
        }

        public MarketInfo GetMarketInfo(string lang)
        {
            var cacheKey = $"{CacheKey.MARKET_INFO_CACHE_KEY}_{lang}";
            var marketInfo = _memoryCacheService.Get<MarketInfo>(cacheKey);
            if (marketInfo is null)
            {
                var markets = GetMarkets();
                marketInfo = markets.FirstOrDefault(m => string.Equals(lang, m.ShortedCode, StringComparison.OrdinalIgnoreCase));

                _memoryCacheService.Set(cacheKey, marketInfo);
            }

            return marketInfo ?? new MarketInfo();
        }

        public async Task<ValidateResult> ValidateLinkFromDatabase(ClickTrackingEvent clickTrackingEvent)
        {
            var command = new ValidateLinkCommand
            {
                AffCode = clickTrackingEvent.AffCode,
                AffDomain = clickTrackingEvent.AffDomain,
                PageCode = clickTrackingEvent.PageCode,
                LanguageCode = clickTrackingEvent.LanguageCode
            };
            await _dynamicRepository.ExecuteAsync(command);

            var validateResult = new ValidateResult
            {
                IsValid = command.ErrCode == 0,
                ErrorMessage = command.ErrMsg,
                RedirectView = command.RedirectPage,
            };
            return validateResult;
        }
    }
}
