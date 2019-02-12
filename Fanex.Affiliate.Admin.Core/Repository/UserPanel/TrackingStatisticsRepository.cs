using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using System.Data;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.Repository.UserPanel
{
    public class TrackingStatisticsRepository : BaseRepository, ITrackingStatisticsRepository
    {
        public IEnumerable<TrackingCode> GetTrackingCodeList(int affId)
        {
            const string SpKey = "Aff_AffiliateSelTrackingCode";
            var result = new List<TrackingCode>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("AffID", DbType.Int32, affId);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new TrackingCode { Name = Convert.ToString(reader["TrackingCode"]) });
                    }
                }
            }

            return result;
        }

        public IEnumerable<TrackingStatistics> GetTrackingStatisticsList(TrackingStatisticsParam param)
        {
            const string SpKey = "Aff_AffiliateSelClickStatisticsAllMonths";
            var result = new List<TrackingStatistics>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignCommonParam(objectDb, param);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(BuildNoteItem(reader));
                    }
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            result.Add(BuildNoteItem(reader));
                        }
                    }
                }
            }

            return result;
        }

        public IEnumerable<TrackingStatistics> GetTrackingStatisticsDetailList(TrackingStatisticsParam param)
        {
            const string SpKey = "Aff_AffiliateSelClickStatisticsMonthDetail";
            var result = new List<TrackingStatistics>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignCommonParam(objectDb, param);
                objectDb.AddInParameter("month", DbType.Int32, param.Month);
                objectDb.AddInParameter("year", DbType.Int32, param.Year);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(BuildNoteItem(reader));
                    }
                }
            }

            return result;
        }

        private static void AssignCommonParam(IObjectDb objectDb, TrackingStatisticsParam param)
        {
            objectDb.AddInParameter("AffID", DbType.Int32, param.AffId);
            objectDb.AddInParameter("TrackingCode", DbType.String, param.TrackingCode);
        }

        private static TrackingStatistics BuildNoteItem(IDataReader reader)
        {
            return new TrackingStatistics
            {
                TotalClick = reader["TotalClick"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TotalClick"]),
                UniqueClick = reader["TotalClick"] == DBNull.Value ? 0 :  Convert.ToInt32(reader["UniqueClick"]),
                Date = Convert.ToDateTime(reader["Date"])
            };
        }
    }
}
