using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Services.UserPanel
{
    public class ViewLogService : BaseRepository, IViewLogService
    {
        public EntityResult<ViewLog> GetViewLogList(ViewLogParam param)
        {
            const string SpKey = "Aff_Admin_AffiliateLog_Sel";
            var result = new EntityResult<ViewLog>();
            var dataList = new List<ViewLog>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("AffID", DbType.Int32, param.AffId);
                objectDb.AddInParameter("LogType", DbType.Int32, param.LogType);
                objectDb.AddInParameter("FromDate", DbType.DateTime, param.FromDate);
                objectDb.AddInParameter("ToDate", DbType.DateTime, param.ToDate);
                objectDb.AddInParameter("PageIndex", DbType.Int32, param.PageIndex);
                objectDb.AddInParameter("PageSize", DbType.Int32, param.PageSize);

                objectDb.AddOutParameter("NRows", DbType.Int32, Size.SizeOfInt);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(new ViewLog
                        {
                            Id = Convert.ToInt32(reader["LogId"]),
                            Date = Convert.ToDateTime(reader["Date"]),
                            UpdatedBy = Convert.ToString(reader["UpdatedName"]),
                            Action = Convert.ToString(reader["Action"]),
                            Description = Convert.ToString(reader["Description"]),
                            Ip = Convert.ToString(reader["IP"]),
                        });
                    }
                }

                result.MainData = dataList;
                var totalRow = objectDb.GetParameterValue("NRows");
                result.TotalItem = totalRow == DBNull.Value ? 0 : Convert.ToInt32(totalRow);
            }

            return result;
        }
    }
}
