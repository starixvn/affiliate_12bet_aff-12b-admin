using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Data;
using System;
using System.Collections.Generic;
using System.Data;

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

                objectDb.SetParameterValue("ReportBy", param.GroupBy);
                objectDb.SetParameterValue("CountryCodes", param.SelectedCountries);
                objectDb.SetParameterValue("LanguageCodes", param.LanguageCode);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = BuildLandingPageStatisticsItem(reader);
                        item.Page = Convert.ToString(reader["Page"]);
                        if (string.Equals("Country", param.GroupBy, StringComparison.OrdinalIgnoreCase))
                        {
                            item.Country = Convert.ToString(reader["Country"]);
                            item.CountryCode = Convert.ToString(reader["CountryCode"]);
                        }
                        else
                        {
                            item.Language = Convert.ToString(reader["Language"]);
                            item.LanguageCode = Convert.ToString(reader["LanguageCode"]);
                        }

                        result.Add(item);
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

                objectDb.SetParameterValue("CountryCode", param.SelectedCountry);
                objectDb.SetParameterValue("LanguageCode", param.LanguageCode);
                objectDb.SetParameterValue("PageIndex", param.PageIndex);
                objectDb.SetParameterValue("PageSize", param.PageSize);

                objectDb.SetParameterValue("NRows", DefaultValue.Int);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = BuildLandingPageStatisticsItem(reader);
                        item.WebsiteUrl = Convert.ToString(reader["URLReferrer"]);

                        dataList.Add(item);
                    }
                }
                result.MainData = dataList;
                result.TotalItem = GetTotalRow(objectDb);
            }

            return result;
        }

        private static void AssignCommonParam(IObjectDb objectDb, LandingPageStatisticsParam param)
        {
            objectDb.SetParameterValue("AffID", param.AffId);
            objectDb.SetParameterValue("FromDate", param.FromDate);
            objectDb.SetParameterValue("ToDate", param.ToDate);
            objectDb.SetParameterValue("SportCode", param.SelectedPage);
            objectDb.SetParameterValue("DeviceType", param.DeviceType);
        }

        private static LandingPageStatistics BuildLandingPageStatisticsItem(IDataReader reader)
        {
            return new LandingPageStatistics
            {
                TotalClick = reader["TotalClick"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TotalClick"]),
                UniqueClick = reader["UniqueClick"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UniqueClick"]),
                Signups = reader["Signups"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Signups"]),
                Fdm = reader["FDM"] == DBNull.Value ? 0 : Convert.ToInt32(reader["FDM"]),
            };
        }
    }
}
