using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO;

namespace Fanex.Affiliate.Admin.Core.Repository.UserPanel
{
    public class ViewLogRepository : BaseRepository, IViewLogRepository
    {
        public EntityResult<ViewLog> GetViewLogList(ViewLogParam param)
        {
            const string SpKey = "Aff_AffiliateSelLog";
            var result = new EntityResult<ViewLog>();
            var dataList = new List<ViewLog>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("AffID", DbType.Int32, param.AffId);
                objectDb.AddInParameter("LogType", DbType.Int32, param.LogType);
                objectDb.AddInParameter("DateFrom", DbType.DateTime, param.FromDate);
                objectDb.AddInParameter("DateTo", DbType.DateTime, param.ToDate);
                objectDb.AddInParameter("PageIndex", DbType.Int32, param.PageIndex);
                objectDb.AddInParameter("PageSize", DbType.Int32, param.PageSize);

                objectDb.AddOutParameter("RowCount", DbType.Int32, 4);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add( new ViewLog
                        {
                            Id = Convert.ToInt32(reader["RefNo"]),
                            Date = Convert.ToDateTime(reader["LoggedDate"]),
                            UpdatedBy = Convert.ToString(reader["UserName"]),
                            Action = Convert.ToString(reader["TypeName"]) + $" ({Convert.ToString(reader["SPName"])})",
                            Description = Convert.ToString(reader["Details"]),
                            Ip = Convert.ToString(reader["IP"]),
                        });
                    }
                }

                result.MainData = dataList;
                var totalRow = objectDb.GetParameterValue("RowCount");
                result.TotalItem = totalRow == DBNull.Value ? 0 : Convert.ToInt32(totalRow);
            }

            return result;
        }
    }
}
