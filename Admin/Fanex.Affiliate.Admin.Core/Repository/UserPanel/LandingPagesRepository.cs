using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Data;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Repository.UserPanel
{
    public class LandingPagesRepository : BaseRepository, ILandingPagesRepository
    {
        public IEnumerable<LandingPages> GetLandingPagesList(LandingPagesParam param)
        {
            param.LanguageCode = GetLanguageCode(param.Language);

            const string SpKey = "Aff_ClickStatisticsSelByPage";
            var result = new List<LandingPages>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignCommonParam(objectDb, param);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(BuildLandingPagesItem(reader, true));
                    }
                }
            }

            return result;
        }

        public EntityResult<LandingPages> GetLandingPagesDetailList(LandingPagesDetailParam param)
        {
            param.LanguageCode = GetLanguageCode(param.Language);
            param.Page = param.SelectedPage;

            const string SpKey = "Aff_ClickStatisticsDetailBySite";
            var result = new EntityResult<LandingPages>();
            var dataList = new List<LandingPages>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignCommonParam(objectDb, param);

                objectDb.AddInParameter("PageIndex", DbType.Int32, param.PageIndex);
                objectDb.AddInParameter("PageSize", DbType.Int32, param.PageSize);

                objectDb.AddOutParameter("NRows", DbType.Int32, 4);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(BuildLandingPagesItem(reader, false));
                    }
                }
                result.MainData = dataList;
                var totalRow = objectDb.GetParameterValue("NRows");
                result.TotalItem = totalRow == DBNull.Value ? 0 : Convert.ToInt32(totalRow);
            }

            return result;
        }

        private static void AssignCommonParam(IObjectDb objectDb, LandingPagesParam param)
        {
            objectDb.AddInParameter("AffID", DbType.Int32, param.AffId);
            objectDb.AddInParameter("FromDate", DbType.String, param.FromDate);
            objectDb.AddInParameter("ToDate", DbType.String, param.ToDate);
            objectDb.AddInParameter("SportCode", DbType.String, param.Page);
            objectDb.AddInParameter("LanguageCode", DbType.String, param.LanguageCode);
            objectDb.AddInParameter("DeviceType", DbType.Int32, param.DeviceType);
        }

        private static LandingPages BuildLandingPagesItem(IDataReader reader, bool isGetLandingPagesList)
        {
            return new LandingPages
            {
                Page = isGetLandingPagesList ? (Convert.ToString(reader["SportCode"])) : string.Empty,
                LanguageCode = isGetLandingPagesList ? (Convert.ToString(reader["LanguageCode"])) : string.Empty,
                Language = isGetLandingPagesList ? GetLanguage(Convert.ToString(reader["LanguageCode"])) : string.Empty,
                WebsiteUrl = isGetLandingPagesList ? string.Empty : (Convert.ToString(reader["URLReferrer"])),
                TotalClick = reader["TotalClick"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TotalClick"]),
                UniqueClick = reader["UniqueClick"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UniqueClick"]),
                Signups = reader["Signups"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Signups"]),
                Fdm = reader["FDM"] == DBNull.Value ? 0 : Convert.ToInt32(reader["FDM"]),
            };
        }

        private static string GetLanguageCode(string language)
        {
            switch (language?.ToLower())
            {
                case "english (europe)": return "eneuro";
                case "english": return "en";
                case "spanish": return "es";
                case "english (india)": return "hien";
                case "indonesian": return "id";
                case "japanese": return "jp";
                case "korean": return "ko";
                case "thai": return "th";
                case "vietnamese": return "vn";
                case "chinese simplified": return "cs";
                case "chinese traditional": return "ch";
                default:
                    return null;
            }
        }

        private static string GetLanguage(string languageCode)
        {
            switch (languageCode?.ToLower())
            {
                case "eneuro": return "English (Europe)";
                case "en": return "English";
                case "es": return "Spanish";
                case "hien": return "English (India)";
                case "id": return "Indonesian";
                case "jp": return "Japanese";
                case "ko": return "Korean";
                case "th": return "Thai";
                case "vn": return "Vietnamese";
                case "cs": return "Chinese Simplified";
                case "ch": return "Chinese Traditional";
                default:
                    return languageCode;
            }
        }
    }
}
