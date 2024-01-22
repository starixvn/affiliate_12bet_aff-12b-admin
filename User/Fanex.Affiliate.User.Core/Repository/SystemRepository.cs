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

        public IEnumerable<Language> GetLanguageList(bool isInternal, int? userId = null)
        {
            const string uSpKey = "Aff_Admin_AdminUser_Market_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpKey, dbMode))
            {
                var languageList = db.Query<Language>(new { CustId = userId });
                return languageList.Where(l => l.Status);
            }
        }

        public IEnumerable<TargetLanguage> GetTargetLanguageListByCountry(bool isInternal, string targetCountryCode = null, int? userId = null)
        {
            const string uSpKey = "Aff_Language_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpKey, dbMode))
            {
                return db.Query<TargetLanguage>(new { userId, targetCountryCode });
            }
        }

        public IEnumerable<TargetCountry> GetTargetCountryList(bool isInternal, int? userId = null)
        {
            const string uSpKey = "Aff_TargetCountry_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpKey, dbMode))
            {
                return db.Query<TargetCountry>(new { userId });
            }
        }
    }
}
