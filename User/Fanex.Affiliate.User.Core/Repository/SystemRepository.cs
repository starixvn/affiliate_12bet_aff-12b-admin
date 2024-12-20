namespace Fanex.Affiliate.User.Core.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Criterias;
    using Data.Repository;
    using DTO;
    using DTO.UserPanel.MediaGallery;

    public class SystemRepository : ISystemRepository
    {
        private readonly IDynamicRepository _repository;

        public SystemRepository(IDynamicRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Language> GetLanguageList(bool isInternal)
        {
            var languages = _repository.Fetch<LanguageType>(new GetLanguageListCriteria(isInternal));

            return languages
                .Select(l => new Language
                {
                    Code = l.MarketId,
                    DisplayName = l.DisplayName,
                    EnglishName = l.MarketName,
                    NativeName = l.MarketNativeName,
                    ShortedCode = l.ShortedCode
                });
        }
    }
}
