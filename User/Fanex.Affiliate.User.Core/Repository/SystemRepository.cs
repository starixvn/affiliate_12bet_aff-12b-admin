using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.System;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.User.Core.Repository
{
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
    }
}
