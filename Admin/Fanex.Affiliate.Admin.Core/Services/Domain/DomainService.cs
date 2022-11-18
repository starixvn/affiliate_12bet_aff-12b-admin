using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Data;
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

        public IEnumerable<DomainInfo> GetDomainList(bool isAffDomain, int userId)
        {
            var spKey = isAffDomain ? "Aff_Admin_AffiliateDomain_Sel" : "Aff_Admin_12BETDomain_Sel";

            using (var objectDb = GetObjectDb(spKey))
            {
                return objectDb.Query<DomainInfo>(new { userId });
            }
        }

        public ResultInfo InsertDomain(InsertUpdateDomainParam param)
        {
            var spKey = param.IsAffDomain ? "Aff_Admin_AffiliateDomain_Ins" : "Aff_Admin_12BETDomain_Ins";

            using (var objectDb = GetObjectDb(spKey))
            {
                AssignParam(objectDb, param);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo UpdateDomain(InsertUpdateDomainParam param)
        {
            var spKey = param.IsAffDomain ? "Aff_Admin_AffiliateDomain_Upd" : "Aff_Admin_12BETDomain_Upd";

            using (var objectDb = GetObjectDb(spKey))
            {
                objectDb.SetParameterValue("DomainId", param.DomainId);
                AssignParam(objectDb, param);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo DeleteDomain(DeleteDomainParam param)
        {
            var spKey = param.IsAffDomain ? "Aff_Admin_AffiliateDomain_Del" : "Aff_Admin_12BETDomain_Del";

            using (var objectDb = GetObjectDb(spKey))
            {
                objectDb.SetParameterValue("DomainId", param.DomainId);
                objectDb.SetParameterValue("userid", param.UserId);
                objectDb.SetParameterValue("ip", param.Ip);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        private static void AssignParam(IObjectDb objectDb, InsertUpdateDomainParam param)
        {
            objectDb.SetParameterValue("DomainName", param.DomainName);
            objectDb.SetParameterValue("IsSSL", param.IsSSL);
            objectDb.SetParameterValue("UserID", param.UserId);
            objectDb.SetParameterValue("IP", param.Ip);

            AssignResultOutputParam(objectDb);
        }
    }
}
