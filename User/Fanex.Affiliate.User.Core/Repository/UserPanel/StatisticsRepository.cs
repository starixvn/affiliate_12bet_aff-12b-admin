using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using Fanex.Affiliate.User.Core.DTO.UserPanel.Statistics;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Common.Extensions;
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

            using (var objectDb = _builder.Build(uSpkey, dbMode))
            {
                using (var reader = objectDb.ExecuteReader(statisticsDetailParam))
                {
                    while (reader.Read())
                    {
                        dataList.Add(new ReferrerStatistics
                        {
                            DomainReferrer = reader.ToString("UrlReferrer"),
                            TotalClick = reader.ToInt(TotalClickColumn),
                            UniqueClick = reader.ToInt(UniqueClickColumn),
                            SignUps = reader.ToInt("Signups"),
                            Fdm = reader.ToInt("FDM")
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
                return db.Query<LandingPage>(new { TargetCountryCode = string.Empty });
            }
        }

        public IEnumerable<MarketingPageStatistics> GetMarketingPageStatistics(MarketingPageStatisticsParam param)
        {
            const string spKey = "Aff_User_Rpt_MarketingPage_Statistics";

            using (var db = _builder.Build(spKey))
            {
                return db.Query<MarketingPageStatistics>(param);
            }
        }
    }
}