namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    using System.Collections.Generic;
    using Commands.Marketing;
    using Criterias;
    using Data.Repository;
    using DTO;
    using Extensions.Mappers;
    using Fanex.Affiliate.User.Core.DTO.UserPanel;
    using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
    using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
    using MetalModels;

    public class MarketingRepository : BaseRepository, IMarketingRepository
    {
        private readonly IDynamicRepository _repository;

        public MarketingRepository(IDynamicRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<CategoryType> GetCategoryList(MediaCategoryParam param, bool isInternal)
        {
            return _repository.Fetch<CategoryType>(new GetCategoryListCriteria(param.TypeId, param.TargetId, isInternal));
        }

        public IEnumerable<CurrencyType> GetCurrencyList(bool isInternal)
        {
            return _repository.Fetch<CurrencyType>(new GetCurrencyListCriteria(isInternal));
        }

        public IEnumerable<SizeType> GetSizeList(SizeParam param, bool isInternal)
        {
            var criteria = new GetSizeListCriteria(isInternal);
            criteria.Map(param);

            return _repository.Fetch<SizeType>(criteria);
        }

        public IEnumerable<Domain> GetAvailableDomainList(int affId, bool isMember, bool isInternal)
        {
            var affCriteria = new GetAffDomainListCriteria(affId, isInternal);
            var subAffCriteria = new GetSubAffDomainListCriteria(isInternal);

            return isMember 
                ? _repository.Fetch<Domain>(affCriteria) 
                : _repository.Fetch<Domain>(subAffCriteria);
        }

        public IEnumerable<MarketingItem> GetBannerList(MarketingBannerParam bannerParam)
        {
            var criteria = new GetBannerListCriteria();
            criteria.Map(bannerParam);

            return _repository.Fetch<MarketingItem>(criteria);
        }

        public IEnumerable<LandingPage> GetLandingPageList(bool isInternal)
        {
            return _repository.Fetch<LandingPage>(new GetLandingPageListCriteria(isInternal));
        }

        public ResultInfo AddMarketingChannel(int userId, string channelCode, string channelName)
        {
            var command = new AddMarketingChannelCommand(userId, channelCode, channelName);
            _repository.Execute(command);

            return new ResultInfo
            {
                IsOk = command.ErrCode == 0,
                Code = command.ErrCode,
                Message = command.ErrMsg
            };
        }

        public IEnumerable<MarketingChannel> GetMarketingChannels(int userId)
        {
            return _repository.Fetch<MarketingChannel>(new GetMarketingChannelsCriteria(userId));
        }

        public ResultInfo ToggleMarketingLink(MarketingLinkParam param)
        {
            var command = new ToggleMarketingLinkCommand();
            command.Map(param);

            _repository.Execute(command);

            return new ResultInfo
            {
                Code = command.ErrCode,
                IsOk = command.ErrCode == 0,
                Message = command.ErrMsg
            };
        }

        public IEnumerable<MarketingChannelLink> GetUsingMarketingLink(int userId)
        {
            return _repository.Fetch<MarketingChannelLink>(new GetUsingMarketingLinkCriteria(userId));
        }

    }
}