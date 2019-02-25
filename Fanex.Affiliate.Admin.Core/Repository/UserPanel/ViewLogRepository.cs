using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Repository.UserPanel
{
    public class ViewLogRepository : BaseRepository, IViewLogRepository
    {
        public IEnumerable<ViewLog> GetViewLogList(ViewLogParam param)
        {
            const string SpKey = "Aff_AffiliateSelLog";
            var result = new List<ViewLog>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("AffID", DbType.Int32, param.AffId);
                objectDb.AddInParameter("LogType", DbType.Int32, param.LogType);
                objectDb.AddInParameter("DateFrom", DbType.DateTime, param.FromDate);
                objectDb.AddInParameter("ToDate", DbType.DateTime, param.ToDate);
                objectDb.AddInParameter("PageIndex", DbType.Int32, param.PageIndex);
                objectDb.AddInParameter("PageSize", DbType.Int32, param.PageSize);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add( new ViewLog
                        {
                            Id = Convert.ToInt32(reader["RefNo"]),
                            Date = Convert.ToDateTime(reader["LoggedDate"]),
                        });
                    }
                }
            }

            return result;
        }
    }
}
