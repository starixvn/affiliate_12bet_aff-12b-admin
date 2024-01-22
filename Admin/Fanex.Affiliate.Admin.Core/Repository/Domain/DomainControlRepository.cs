using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Repository.Domain
{
    public class DomainControlRepository : BaseRepository, IDomainControlRepository
    {
        public IEnumerable<DomainControlInfo> GetDomainControlList(string targetCountryCodes)
        {
            const string SpKey = "Aff_Admin_DomainControl_Sel";

            using (var objectDb = GetObjectDb(SpKey))
            {
                return objectDb.Query<DomainControlInfo>(new { targetCountryCodes });
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

        public ResultInfo InsertDomainControl(InsertDomainControlParam param)
        {
            const string SpKey = "Aff_Admin_DomainControl_Ins";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.ExecuteNonQuery(param);

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo UpdateDomainControl(UpdateDomainControlParam param)
        {
            const string SpKey = "Aff_Admin_DomainControl_Upd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.ExecuteNonQuery(param);

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo DeleteDomainControl(DeleteDomainControlParam param)
        {
            const string SpKey = "Aff_Admin_DomainControl_Del";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.ExecuteNonQuery(param);

                return GetResultInfo(objectDb);
            }
        }
    }
}
