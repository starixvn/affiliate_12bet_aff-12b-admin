using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO;

namespace Fanex.Affiliate.User.Core.Repository
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly IObjectDBBuilder _builder;

        public LanguageRepository(IObjectDBBuilder builder)
        {
            _builder = builder;
        }

        public IEnumerable<Language> GetLanguageList(bool isInternal, int? adminId = null)
        {
            const string uSpKey = "Aff_Admin_AdminUser_Market_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpKey, dbMode))
            {
                return db.Query<Language>(new { CustId = adminId });
            }
        }
    }
}
