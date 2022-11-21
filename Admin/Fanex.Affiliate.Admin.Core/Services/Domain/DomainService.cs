using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Repository;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Domain
{
    public class DomainService : BaseRepository, IDomainService
    {
        public IEnumerable<Market> GetMarketList(MarketParam param)
        {
            const string SpKey = "Aff_SupportBlockMarket_Sel";

            using (var objectDb = GetObjectDb(SpKey))
            {
                return objectDb.Query<Market>(param);
            }
        }

        public IEnumerable<DomainInfo> GetDomainList(int userId, bool isAffDomain = true)
        {
            var spKey = isAffDomain ? "Aff_Admin_AffiliateDomain_Sel" : "Aff_Admin_12BETDomain_Sel";

            using (var objectDb = GetObjectDb(spKey))
            {
                return objectDb.Query<DomainInfo>(new { userId });
            }
        }

        public DomainInfo GetDomainById(int userId, int domainId, bool isAffDomain = true)
        {
            var spKey = isAffDomain ? "Aff_Admin_AffiliateDomain_Sel" : "Aff_Admin_12BETDomain_Sel";

            using (var objectDb = GetObjectDb(spKey))
            {
                return objectDb.QueryEntity<DomainInfo>(new { userId, domainId });
            }
        }

        public ResultInfo InsertDomain(InsertDomainParam param)
        {
            var spKey = param.IsAffDomain ? "Aff_Admin_AffiliateDomain_Ins" : "Aff_Admin_12BETDomain_Ins";

            using (var objectDb = GetObjectDb(spKey))
            {
                objectDb.ExecuteNonQuery(param);

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo UpdateDomain(UpdateDomainParam param)
        {
            var spKey = param.IsAffDomain ? "Aff_Admin_AffiliateDomain_Upd" : "Aff_Admin_12BETDomain_Upd";

            using (var objectDb = GetObjectDb(spKey))
            {
                objectDb.ExecuteNonQuery(param);

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo DeleteDomain(DeleteDomainParam param)
        {
            var spKey = param.IsAffDomain ? "Aff_Admin_AffiliateDomain_Del" : "Aff_Admin_12BETDomain_Del";

            using (var objectDb = GetObjectDb(spKey))
            {
                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery(param);

                return GetResultInfo(objectDb);
            }
        }
    }
}
