using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.NewViewLog;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.NewViewLog;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Data;
using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.NewViewLog
{
    public class NewViewLogService : BaseRepository, INewViewLogService
    {
        public IEnumerable<LogAction> GetLogActionList()
        {
            const string SpKey = "Aff_LogAction_Sel";
            var dataList = new List<LogAction>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(new LogAction
                        {
                            ActionIds = Convert.ToString(reader["ActionIds"]),
                            ActionName = Convert.ToString(reader["ActionName"]),
                        });
                    }
                }
            }

            return dataList;
        }

        public EntityResult<NewViewLogInfo> GetNewViewLogList(NewViewLogParam param)
        {
            const string SpKey = "Aff_Admin_ViewLog_Sel";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("UserId", param.UserId);
                objectDb.SetParameterValue("AffName", param.AffName);
                AssignParam(objectDb, param);

                return new EntityResult<NewViewLogInfo>
                {
                    MainData = objectDb.Query<NewViewLogInfo>(),
                    TotalItem = GetTotalRow(objectDb)
                };
            }
        }

        public EntityResult<NewViewLogDetailInfo> GetNewViewLogDetailList(NewViewLogParam param)
        {
            const string SpKey = "Aff_Admin_AffiliateLog_Sel";
            var result = new EntityResult<NewViewLogDetailInfo>();
            var dataList = new List<NewViewLogDetailInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("AffID", param.AffId);
                AssignParam(objectDb, param);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(new NewViewLogDetailInfo
                        {
                            LogId = Convert.ToInt32(reader["LogId"]),
                            Date = Convert.ToDateTime(reader["Date"]),
                            OldData = XmlHelper.ParseXmlToJson(Convert.ToString(reader["OldData"])),
                            NewData = XmlHelper.ParseXmlToJson(Convert.ToString(reader["NewData"])),
                            UpdatedName = Convert.ToString(reader["UpdatedName"]),
                            Action = Convert.ToString(reader["Action"]),
                            Ip = Convert.ToString(reader["IP"]),
                        });
                    }
                }

                result.MainData = dataList;
                result.TotalItem = GetTotalRow(objectDb);
            }

            return result;
        }

        private static void AssignParam(IObjectDb objectDb, NewViewLogParam param)
        {
            objectDb.SetParameterValue("LogActionIds", param.LogActionIds);
            objectDb.SetParameterValue("FromDate", param.FromDate);
            objectDb.SetParameterValue("ToDate", param.ToDate);
            objectDb.SetParameterValue("PageIndex", param.PageIndex);
            objectDb.SetParameterValue("PageSize", param.PageSize);
            objectDb.SetParameterValue("NRows", DefaultValue.Int);
        }
    }
}
