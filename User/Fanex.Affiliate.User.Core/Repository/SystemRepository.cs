namespace Fanex.Affiliate.User.Core.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using DTO.UserPanel.MediaGallery;
    using Fanex.Affiliate.User.Core.DTO;
    using Fanex.Affiliate.User.Core.DTO.System;

    public class SystemRepository : ISystemRepository
    {
        private readonly IObjectDBBuilder _builder;

        public SystemRepository(IObjectDBBuilder builder)
        {
            _builder = builder;
        }

        public IEnumerable<TargetLanguage> GetTargetLanguageListByCountry(bool isInternal, string targetCountryCode = null, int? userId = null)
        {
            return new List<TargetLanguage>();
        }

        public IEnumerable<TargetCountry> GetTargetCountryList(bool isInternal, int? userId = null)
        {
            return new List<TargetCountry>();
        }

        public IEnumerable<Language> GetLanguageList(bool isInternal)
        {
            const string uSpKey = "Aff_Market_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var objectDb = _builder.Build(uSpKey, dbMode))
            {
                return objectDb.Query<LanguageType>()
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
}
