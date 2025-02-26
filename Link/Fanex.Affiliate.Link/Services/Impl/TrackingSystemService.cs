namespace Fanex.Affiliate.Link.Services.Impl
{
    using Fanex.Affiliate.Link.Commands;
    using Fanex.Affiliate.Link.Criterias;
    using Fanex.Affiliate.Link.DTOs;
    using Fanex.Affiliate.Link.Services.Interface;
    using Fanex.Caching.Memory;
    using Fanex.Data.Repository;
    using System.Collections.Generic;

    public class TrackingSystemService : ITrackingSystemService
    {
        private const string MEDIA_LINK_CACHE_KEY = "Aff.12B.Go.MediaLink";

        private readonly IDynamicRepository _dynamicRepository;
        private readonly IMemoryCacheService _memoryCacheService;

        public TrackingSystemService()
            : this(null, null)
        {
        }

        private TrackingSystemService(IDynamicRepository dynamicRepository,
            IMemoryCacheService memoryCacheService)
        {
            _dynamicRepository = dynamicRepository ?? new DynamicRepository();
            _memoryCacheService = memoryCacheService ?? new MemoryCacheService(new MemoryCache(new MemoryCacheOptions()));
        }

        public string InsertStatistic(TrackingSystemCommand command)
        {
            _dynamicRepository.Execute(command);

            return command.RedirectPage;
        }

        public bool IsTrackingMarketingLink(CheckMarketingLinkCommand command)
        {
            var cacheKey = $"{MEDIA_LINK_CACHE_KEY}_{command.AffMediaLink}";
            var isExistedTracking = _memoryCacheService.Get<bool>(cacheKey);
            if (!isExistedTracking)
            {
                _dynamicRepository.Execute(command);
                _memoryCacheService.Set(cacheKey, command.IsExistedTracking);

                return command.IsExistedTracking;
            }

            return isExistedTracking;
        }

        public bool ToggleMarketingLink(ToggleMarketingLinkCommand command)
        {
            _dynamicRepository.Execute(command);

            return command.ErrCode == 0;
        }
    }
}
