using System;
using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly IObjectDBBuilder _builder;

        private const string UniqueClickColumn = "UniqueClick";
        private const string TotalClickColumn = "TotalClick";

        public StatisticsRepository(IObjectDBBuilder builder)
        {
            _builder = builder;
        }

        public IEnumerable<string> GetTrackingCode(int affId, bool isInternal)
        {
            const string uSpkey = "Aff_AffiliateSelTrackingCode";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpkey, dbMode))
            {
                return db.Query<string>(new { affId });
            }
        }

        public IEnumerable<TrackingStatistics> GetTrackingStatistics(int affId, string trackingCode, bool isInternal)
        {
            const string uSpkey = "Aff_AffiliateSelClickStatisticsAllMonths";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;
            var dataList = new List<TrackingStatistics>();

            using (var db = _builder.Build(uSpkey, dbMode))
            {
                using (var multipleReader = db.QueryMultiple(new { affId, trackingCode }))
                {
                    while (!multipleReader.IsEmptyOrClosed)
                    {
                        dataList.AddRange(multipleReader.Read<TrackingStatistics>());
                    }
                }
            }

            return dataList;
        }

        public IEnumerable<TrackingStatistics> GetTrackingStatisticsDetail(TrackingStatisticsDetailParam trackingStatisticsDetailParam)
        {
            const string uSpkey = "Aff_AffiliateSelClickStatisticsMonthDetail";
            var dbMode = trackingStatisticsDetailParam.IsInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpkey, dbMode))
            {
                return db.Query<TrackingStatistics>(trackingStatisticsDetailParam);
            }
        }

        public IEnumerable<ReferrerStatistics> GetReferrerStatistics(ReferrerStatisticsParam referrerStatisticsParam)
        {
            const string uSpkey = "Aff_ClickStatisticsSelBySite";
            var dbMode = referrerStatisticsParam.IsInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpkey, dbMode))
            {
                return db.Query<ReferrerStatistics>(referrerStatisticsParam);
            }
        }

        public IEnumerable<ReferrerStatistics> GetStatisticsDetailBySite(StatisticsDetailBySiteParam statisticsDetailParam)
        {
            const string uSpkey = "Aff_ClickStatisticsDetailBySite";
            var dbMode = statisticsDetailParam.IsInternal ? DBMode.View : DBMode.Main;
            var dataList = new List<ReferrerStatistics>();

            using (var db = _builder.Build(uSpkey, dbMode))
            {
                using (var reader = db.ExecuteReader(statisticsDetailParam))
                {
                    while (reader.Read())
                    {
                        dataList.Add(new ReferrerStatistics
                        {
                            DomainReferrer = Convert.ToString(reader["UrlReferrer"]),
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

            using (var db = _builder.Build(uSpKey, dbMode))
            {
                return db.Query<LandingPageStatistics>(landingPageStatisticsParam);
            }
        }

        public AccountOverviewStatistics GetStatisticsOverview(StatisticsOverviewParam overviewParam)
        {
            const string uSpKey = "Aff_User_StatisticsSel";
            var dbMode = overviewParam.IsInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpKey, dbMode))
            {
                return db.QueryEntity<AccountOverviewStatistics>(overviewParam);
            }
        }
    }
}