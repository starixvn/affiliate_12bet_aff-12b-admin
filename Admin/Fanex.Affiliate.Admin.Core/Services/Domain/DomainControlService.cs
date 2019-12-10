using Fanex.Affiliate.Admin.Core.Repository;
using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Data;
using System.Data;
using Fanex.Affiliate.Admin.Core.Extensions;

namespace Fanex.Affiliate.Admin.Core.Services.Domain
{
    public class DomainControlService : BaseRepository, IDomainControlService
    {
        public IEnumerable<DomainControlInfo> GetDomainControlList(string market)
        {
            const string SpKey = "Aff_Admin_DomainControl_Sel";

            var result = new List<DomainControlInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("MarketId", DbType.String, market);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new DomainControlInfo
                        {
                            DomainId = Convert.ToInt32(reader["DomainControlId"]),
                            MarketId = Convert.ToString(reader["MarketId"]),
                            Market = Convert.ToString(reader["MarketName"]),
                            AffDomainId = reader.GetVal<int>("AffDomainId"),
                            AffDomain = Convert.ToString(reader["AffDomain"]),
                            RedirectDomainId = reader.GetVal<int>("RedirectDomainId"),
                            RedirectDomain = Convert.ToString(reader["RedirectDomain"]),
                            IsUsed = reader.GetVal<bool>("Status"),
                            Creator = Convert.ToString(reader["Creator"]),
                            CreatedDate = reader["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["CreatedDate"])
                        });
                    }
                }
            }

            return result;
        }

        public ResultInfo InsertDomainControl(InsertUpdateDomainControlParam param)
        {
            const string SpKey = "Aff_Admin_DomainControl_Ins";

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignParam(objectDb, param);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo UpdateDomainControl(InsertUpdateDomainControlParam param)
        {
            const string SpKey = "Aff_Admin_DomainControl_Upd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("DomainControlId", DbType.Int32, param.DomainId);

                AssignParam(objectDb, param);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo DeleteDomainControl(DeleteDomainControlParam param)
        {
            const string SpKey = "Aff_Admin_DomainControl_Del";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("DomainControlId", DbType.Int32, param.DomainId);
                objectDb.AddInParameter("userid", DbType.Int32, param.UserId);
                objectDb.AddInParameter("ip", DbType.String, param.Ip);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        private static void AssignParam(IObjectDb objectDb, InsertUpdateDomainControlParam param)
        {
            objectDb.AddInParameter("AffDomainID", DbType.Int32, param.AffDomainId);
            objectDb.AddInParameter("12BetDomainID", DbType.Int32, param.RedirectDomainId);
            objectDb.AddInParameter("MarketID", DbType.String, param.MarketId);
            objectDb.AddInParameter("Status", DbType.Int32, param.IsUsed);
            objectDb.AddInParameter("UserID", DbType.Int32, param.UserId);
            objectDb.AddInParameter("IP", DbType.String, param.Ip);

            AssignResultOutputParam(objectDb);
        }
    }
}
