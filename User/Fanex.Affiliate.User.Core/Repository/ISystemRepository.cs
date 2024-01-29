using Fanex.Affiliate.User.Core.DTO.System;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.Repository
{
    public interface ISystemRepository
    {
        IEnumerable<TargetLanguage> GetTargetLanguageListByCountry(bool isInternal, string targetCountryCode = null, int? userId = null);

        IEnumerable<TargetCountry> GetTargetCountryList(bool isInternal, int? userId = null);
    }
}
