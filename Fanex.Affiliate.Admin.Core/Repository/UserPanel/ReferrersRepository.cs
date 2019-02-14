using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Data;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO;

namespace Fanex.Affiliate.Admin.Core.Repository.UserPanel
{
    public class ReferrersRepository : BaseRepository, IReferrersRepository
    {
        public IEnumerable<Referrers> GetReferrersAllSites(ReferrersParam param)
        {
            const string SpKey = "Aff_ClickStatisticsSelBySite";
            var result = new List<Referrers>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignCommonParam(objectDb, param);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(BuildReferrersItem(reader, false));
                    }
                }
            }

            return result;
        }

        public EntityResult<Referrers> GetReferrersBySite(ReferrersDetailParam param)
        {
            const string SpKey = "Aff_ClickStatisticsDetailBySite";
            var result = new EntityResult<Referrers>();
            var dataList = new List<Referrers>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignCommonParam(objectDb, param);
                objectDb.AddInParameter("Domain", DbType.String, param.SiteUrl);
                objectDb.AddInParameter("SportCode", DbType.String, string.Empty);
                objectDb.AddInParameter("LanguageCode", DbType.String, string.Empty);
                objectDb.AddInParameter("PageIndex", DbType.Int32, param.PageIndex);
                objectDb.AddInParameter("PageSize", DbType.Int32, param.PageSize);

                objectDb.AddOutParameter("NRows", DbType.Int32, 4);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(BuildReferrersItem(reader, true));
                    }
                }
                result.MainData = dataList;
                var totalRow = objectDb.GetParameterValue("NRows");
                result.TotalItem = totalRow == DBNull.Value ? 0 : Convert.ToInt32(totalRow);
            }

            return result;
        }

        private static void AssignCommonParam(IObjectDb objectDb, ReferrersParam param)
        {
            objectDb.AddInParameter("AffID", DbType.Int32, param.AffId);
            objectDb.AddInParameter("FromDate", DbType.String, param.FromDate);
            objectDb.AddInParameter("ToDate", DbType.String, param.ToDate);
            objectDb.AddInParameter("DeviceType", DbType.Int32, param.DeviceType);

            objectDb.AddOutParameter("TotalClicks", DbType.Int32, 4);
            objectDb.AddOutParameter("UniqueClicks", DbType.Int32, 4);
            objectDb.AddOutParameter("TotalSignUps", DbType.Int32, 4);
            objectDb.AddOutParameter("TotalFDMs", DbType.Int32, 4);
        }

        private static Referrers BuildReferrersItem(IDataReader reader, bool isGetReferrersBySite)
        {
            return new Referrers
            {
                SiteUrl = isGetReferrersBySite ? string.Empty : (reader["DomainReferrer"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DomainReferrer"])),
                WebsiteUrl = isGetReferrersBySite ?  (reader["URLReferrer"] == DBNull.Value ? string.Empty : Convert.ToString(reader["URLReferrer"])) : string.Empty,
                TotalClick = reader["TotalClick"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TotalClick"]),
                UniqueClick = reader["UniqueClick"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UniqueClick"]),
                Signups = reader["Signups"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Signups"]),
                Fdm = reader["FDM"] == DBNull.Value ? 0 : Convert.ToInt32(reader["FDM"]),
            };
        }
    }
}
