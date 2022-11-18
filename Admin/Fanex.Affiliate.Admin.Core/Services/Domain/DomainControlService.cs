using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Data;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Domain
{
    public class DomainControlService : BaseRepository, IDomainControlService
    {
        public IEnumerable<DomainControlInfo> GetDomainControlList(string marketId)
        {
            const string SpKey = "Aff_Admin_DomainControl_Sel";

            using (var objectDb = GetObjectDb(SpKey))
            {
                return objectDb.Query<DomainControlInfo>(new { marketId });
            }
        }

        public DomainControlInfo GetDomainControlById(int domainControlId)
        {
            const string SpKey = "Aff_Admin_DomainControl_Sel";

            using (var objectDb = GetObjectDb(SpKey))
            {
                return objectDb.QueryEntity<DomainControlInfo>(new { domainControlId });
            }
        }

        public ResultInfo InsertDomainControl(InsertUpdateDomainControlParam param)
        {
            const string SpKey = "Aff_Admin_DomainControl_Ins";

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignParam(objectDb, param);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo UpdateDomainControl(InsertUpdateDomainControlParam param)
        {
            const string SpKey = "Aff_Admin_DomainControl_Upd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("DomainControlId", param.DomainControlId);

                AssignParam(objectDb, param);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo DeleteDomainControl(DeleteDomainControlParam param)
        {
            const string SpKey = "Aff_Admin_DomainControl_Del";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("DomainControlId", param.DomainControlId);
                objectDb.SetParameterValue("userid", param.UserId);
                objectDb.SetParameterValue("ip", param.Ip);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        private static void AssignParam(IObjectDb objectDb, InsertUpdateDomainControlParam param)
        {
            objectDb.SetParameterValue("AffDomainID", param.AffDomainId);
            objectDb.SetParameterValue("12BetDomainID", param.RedirectDomainId);
            objectDb.SetParameterValue("MarketID", param.MarketId);
            objectDb.SetParameterValue("Status", param.Status);
            objectDb.SetParameterValue("UserID", param.UserId);
            objectDb.SetParameterValue("IP", param.Ip);

            AssignResultOutputParam(objectDb);
        }
    }
}
