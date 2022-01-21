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
    public class ReferrersRepository : BaseRepository, IReferrersRepository
    {
        public IEnumerable<Referrers> GetReferrersList(ReferrersParam param)
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
                        result.Add(BuildReferrersItem(reader, true));
                    }
                }
            }

            return result;
        }

        public EntityResult<Referrers> GetReferrersDetailList(ReferrersDetailParam param)
        {
            const string SpKey = "Aff_ClickStatisticsDetailBySite";
            var result = new EntityResult<Referrers>();
            var dataList = new List<Referrers>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignCommonParam(objectDb, param);
                objectDb.SetParameterValue("Domain", param.SelectedSiteUrl);
                objectDb.SetParameterValue("PageIndex", param.PageIndex);
                objectDb.SetParameterValue("PageSize", param.PageSize);

                objectDb.SetParameterValue("NRows", DefaultValue.Int);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(BuildReferrersItem(reader, false));
                    }
                }
                result.MainData = dataList;
                result.TotalItem = GetTotalRow(objectDb);
            }

            return result;
        }

        private static void AssignCommonParam(IObjectDb objectDb, ReferrersParam param)
        {
            objectDb.SetParameterValue("AffID", param.AffId);
            objectDb.SetParameterValue("FromDate", param.FromDate);
            objectDb.SetParameterValue("ToDate", param.ToDate);
            objectDb.SetParameterValue("DeviceType", param.DeviceType);
        }

        private static Referrers BuildReferrersItem(IDataReader reader, bool isGetReferrersList)
        {
            return new Referrers
            {
                SiteUrl = isGetReferrersList ? (Convert.ToString(reader["DomainReferrer"])) : string.Empty,
                WebsiteUrl = isGetReferrersList ? string.Empty : (Convert.ToString(reader["URLReferrer"])),
                TotalClick = reader["TotalClick"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TotalClick"]),
                UniqueClick = reader["UniqueClick"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UniqueClick"]),
                Signups = reader["Signups"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Signups"]),
                Fdm = reader["FDM"] == DBNull.Value ? 0 : Convert.ToInt32(reader["FDM"]),
            };
        }
    }
}
