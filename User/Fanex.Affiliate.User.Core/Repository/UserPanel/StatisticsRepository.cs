using System;
using System.Collections.Generic;
using System.Data;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private const string UniqueClickColumn = "UniqueClick";

        private const string TotalClickColumn = "TotalClick";

        public IEnumerable<string> GetTrackingCode(int affId, bool isInternal)
        {
            const string uSpkey = "Aff_AffiliateSelTrackingCode";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var objectDb = new ObjectDBBuilder(uSpkey, dbMode))
            {
                return objectDb.DB.Query<string>(new { affId });
            }
        }

        public IEnumerable<TrackingStatistics> GetTrackingStatistics(int affId, string trackingCode, bool isInternal)
        {
            const string uSpkey = "Aff_AffiliateSelClickStatisticsAllMonths";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;
            var dataList = new List<TrackingStatistics>();

            using (var objectDb = new ObjectDBBuilder(uSpkey, dbMode))
            {
                using (var reader = objectDb.DB.ExecuteReader(new { affId, trackingCode }))
                {
                    while (reader.Read())
                    {
                        dataList.Add(BuildTrackingStatisticsItem(reader));
                    }

                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            dataList.Add(BuildTrackingStatisticsItem(reader));
                        }
                    }
                }
            }

            return dataList;
        }

        public IEnumerable<TrackingStatistics> GetTrackingStatisticsDetail(TrackingStatisticsDetailParam trackingStatisticsDetailParam)
        {
            const string uSpkey = "Aff_AffiliateSelClickStatisticsMonthDetail";
            var dbMode = trackingStatisticsDetailParam.IsInternal ? DBMode.View : DBMode.Main;

            using (var objectDb = new ObjectDBBuilder(uSpkey, dbMode))
            {
                return objectDb.DB.Query<TrackingStatistics>(trackingStatisticsDetailParam);
            }
        }

        public IEnumerable<ReferrerStatistics> GetReferrerStatistics(ReferrerStatisticsParam referrerStatisticsParam)
        {
            const string uSpkey = "Aff_ClickStatisticsSelBySite";
            var dbMode = referrerStatisticsParam.IsInternal ? DBMode.View : DBMode.Main;

            using (var objectDb = new ObjectDBBuilder(uSpkey, dbMode))
            {
                return objectDb.DB.Query<ReferrerStatistics>(referrerStatisticsParam);
            }
        }

        public IEnumerable<ReferrerStatistics> GetStatisticsDetailBySite(StatisticsDetailBySiteParam statisticsDetailParam)
        {
            const string uSpkey = "Aff_ClickStatisticsDetailBySite";
            var dbMode = statisticsDetailParam.IsInternal ? DBMode.View : DBMode.Main;
            var dataList = new List<ReferrerStatistics>();

            using (var objectDb = new ObjectDBBuilder(uSpkey, dbMode))
            {
                using (var reader = objectDb.DB.ExecuteReader(statisticsDetailParam))
                {
                    while (reader.Read())
                    {
                        dataList.Add(new ReferrerStatistics
                        {
                            DomainReferrer = reader["UrlReferrer"].ToString(),
                            TotalClick = reader[TotalClickColumn] == DBNull.Value ? 0 : Convert.ToInt32(reader[TotalClickColumn]),
                            UniqueClick = reader[UniqueClickColumn] == DBNull.Value ? 0 : Convert.ToInt32(reader[UniqueClickColumn]),
                            SignUps = reader["Signups"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Signups"]),
                            Fdm = reader["FDM"] == DBNull.Value ? 0 : Convert.ToInt32(reader["FDM"])
                        });
                    }
                }
            }

            return dataList;
        }

        public IEnumerable<LandingPageStatistics> GetLandingPageStatistics(LandingPageStatisticsParam landingPageStatisticsParam)
        {
            const string uSpKey = "Aff_ClickStatisticsSelByPage";
            var dbMode = landingPageStatisticsParam.IsInternal ? DBMode.View : DBMode.Main;

            using (var objectDb = new ObjectDBBuilder(uSpKey, dbMode))
            {
                return objectDb.DB.Query<LandingPageStatistics>(landingPageStatisticsParam);
            }
        }

        public AccountOverviewStatistics GetStatisticsOverview(StatisticsOverviewParam overviewParam)
        {
            const string uSpKey = "Aff_User_StatisticsSel";
            var dbMode = overviewParam.IsInternal ? DBMode.View : DBMode.Main;

            using (var objectDb = new ObjectDBBuilder(uSpKey, dbMode))
            {
                return objectDb.DB.QueryEntity<AccountOverviewStatistics>(overviewParam);
            }
        }

        private TrackingStatistics BuildTrackingStatisticsItem(IDataReader reader)
        {
            var date = Convert.ToDateTime(reader["Date"]);

            return new TrackingStatistics
            {
                Date = date,
                TotalClick = reader[TotalClickColumn] == DBNull.Value ? 0 : Convert.ToInt32(reader[TotalClickColumn]),
                UniqueClick = reader[UniqueClickColumn] == DBNull.Value ? 0 : Convert.ToInt32(reader[UniqueClickColumn])
            };
        }
    }
}