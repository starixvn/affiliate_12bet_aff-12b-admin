using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Data;
using System.Data;
using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.Repository.UserPanel
{
    public class LandingPageStatisticsRepository : BaseRepository, ILandingPageStatisticsRepository
    {
        public IEnumerable<LandingPageStatistics> GetLandingPageStatisticsList(LandingPageStatisticsParam param)
        {
            const string SpKey = "Aff_ClickStatisticsSelByPage";
            var result = new List<LandingPageStatistics>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignCommonParam(objectDb, param);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(BuildLandingPageStatisticsItem(reader, true));
                    }
                }
            }

            return result;
        }

        public EntityResult<LandingPageStatistics> GetLandingPageStatisticsDetailList(LandingPageStatisticsDetailParam param)
        {
            const string SpKey = "Aff_ClickStatisticsDetailBySite";
            var result = new EntityResult<LandingPageStatistics>();
            var dataList = new List<LandingPageStatistics>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignCommonParam(objectDb, param);

                objectDb.AddInParameter("PageIndex", DbType.Int32, param.PageIndex);
                objectDb.AddInParameter("PageSize", DbType.Int32, param.PageSize);

                objectDb.AddOutParameter("NRows", DbType.Int32, Size.SizeOfInt);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(BuildLandingPageStatisticsItem(reader, false));
                    }
                }
                result.MainData = dataList;
                var totalRow = objectDb.GetParameterValue("NRows");
                result.TotalItem = totalRow == DBNull.Value ? 0 : Convert.ToInt32(totalRow);
            }

            return result;
        }

        private static void AssignCommonParam(IObjectDb objectDb, LandingPageStatisticsParam param)
        {
            objectDb.AddInParameter("AffID", DbType.Int32, param.AffId);
            objectDb.AddInParameter("FromDate", DbType.DateTime, param.FromDate);
            objectDb.AddInParameter("ToDate", DbType.DateTime, param.ToDate);
            objectDb.AddInParameter("SportCode", DbType.String, param.SelectedPage);
            objectDb.AddInParameter("LanguageCode", DbType.String, param.LanguageCode);
            objectDb.AddInParameter("DeviceType", DbType.Int16, param.DeviceType);
        }

        private static LandingPageStatistics BuildLandingPageStatisticsItem(IDataReader reader, bool isGetLandingPageStatisticsList)
        {
            return new LandingPageStatistics
            {
                Page = isGetLandingPageStatisticsList ? (Convert.ToString(reader["SportCode"])) : string.Empty,
                LanguageCode = isGetLandingPageStatisticsList ? (Convert.ToString(reader["LanguageCode"])) : string.Empty,
                WebsiteUrl = isGetLandingPageStatisticsList ? string.Empty : (Convert.ToString(reader["URLReferrer"])),
                TotalClick = reader["TotalClick"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TotalClick"]),
                UniqueClick = reader["UniqueClick"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UniqueClick"]),
                Signups = reader["Signups"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Signups"]),
                Fdm = reader["FDM"] == DBNull.Value ? 0 : Convert.ToInt32(reader["FDM"]),
            };
        }
    }
}
