using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using Fanex.Affiliate.User.Core.DTO.UserPanel.Statistics;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using System;
using System.Collections.Generic;

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
                            SiteUrls = Convert.ToString(reader["UrlReferrer"]),
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

        public IEnumerable<Country> GetCountryList(bool isInternal)
        {
            const string uSpKey = "Aff_Country_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpKey, dbMode))
            {
                return db.Query<Country>();
            }
        }

        public IEnumerable<LandingPage> GetLandingPageList(bool isInternal)
        {
            const string SpKey = "Aff_User_LandingPage_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(SpKey, dbMode))
            {
                return db.Query<LandingPage>(new { TargetCountryCode = string.Empty});
            }
        }
    }
}