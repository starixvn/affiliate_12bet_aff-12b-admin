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
        public IEnumerable<string> GetTrackingCode(int affId, bool isInternal)
        {
            const string uSpkey = "Aff_AffiliateSelTrackingCode";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var objectDb = new ObjectDBBuilder(uSpkey, dbMode))
            {
                return objectDb.DB.Query<string>(new { affId });
            }
        }

        public IEnumerable<TrackingStatistics> GetTrackingStatistics(int affId, string trackingCode, string currentLanguage, bool isInternal)
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
                        dataList.Add(BuildTrackingStatisticsItem(reader, currentLanguage));
                    }

                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            dataList.Add(BuildTrackingStatisticsItem(reader, currentLanguage));
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
                            TotalClick = reader["TotalClick"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TotalClick"]),
                            UniqueClick = reader["UniqueClick"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UniqueClick"]),
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

        private TrackingStatistics BuildTrackingStatisticsItem(IDataReader reader, string currentLanguage)
        {
            var date = Convert.ToDateTime(reader["Date"]);

            return new TrackingStatistics
            {
                Date = date,
                Month = currentLanguage.Equals("id-ID") ? date.ToString("MMMM yyyy", new System.Globalization.CultureInfo(currentLanguage)) : date.ToString("MMMM-yyyy"),
                TotalClick = reader["TotalClick"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TotalClick"]),
                UniqueClick = reader["UniqueClick"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UniqueClick"])
            };
        }
    }
}