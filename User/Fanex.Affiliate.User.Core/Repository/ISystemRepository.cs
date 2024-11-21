namespace Fanex.Affiliate.User.Core.Repository
{
    using System.Collections.Generic;
    using DTO;
    using Fanex.Affiliate.User.Core.DTO.System;

    public interface ISystemRepository
    {
        IEnumerable<TargetLanguage> GetTargetLanguageListByCountry(bool isInternal, string targetCountryCode = null, int? userId = null);

        IEnumerable<TargetCountry> GetTargetCountryList(bool isInternal, int? userId = null);

        IEnumerable<Language> GetLanguageList(bool isInternal);
    }
}
