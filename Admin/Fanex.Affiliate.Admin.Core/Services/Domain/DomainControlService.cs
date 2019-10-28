using Fanex.Affiliate.Admin.Core.Repository;
using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Data;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Services.Domain
{
    public class DomainControlService : BaseRepository, IDomainControlService
    {
        public IEnumerable<DomainControlInfo> GetDomainControlList(string market)
        {
            const string SpKey = "Aff_DomainControlSel";

            var result = new List<DomainControlInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("MarketId", DbType.String, string.IsNullOrEmpty(market) ? null : market);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new DomainControlInfo
                        {
                            DomainId = Convert.ToInt32(reader["Id"]),
                            MarketId = Convert.ToString(reader["MarketId"]),
                            Market = Convert.ToString(reader["MarketName"]),
                            AffDomainId = Convert.ToInt32(reader["AffDomainId"]),
                            AffDomain = Convert.ToString(reader["AffDomain"]),
                            RedirectDomainId = Convert.ToInt32(reader["RedirectDomainId"]),
                            RedirectDomain = Convert.ToString(reader["RedirectDomain"]),
                            IsUsed = Convert.ToBoolean(reader["Status"]),
                            Creator = Convert.ToString(reader["Creator"]),
                            CreatedDate = Convert.ToDateTime(reader["CreateDate"])
                        });
                    }
                }
            }

            return result;
        }

        public ResultInfo InsertDomainControl(InsertUpdateDomainControlParam param)
        {
            const string SpKey = "Aff_DomainControlIns";

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignParam(objectDb, param);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo UpdateDomainControl(InsertUpdateDomainControlParam param)
        {
            const string SpKey = "Aff_DomainControlUpd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignParam(objectDb, param);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo DeleteDomainControl(DeleteDomainControlParam param)
        {
            const string SpKey = "Aff_DomainControlDel";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("DomainId", DbType.Int32, param.DomainId);
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
            objectDb.AddInParameter("MarketID", DbType.String, param.Market);
            objectDb.AddInParameter("Status", DbType.Int32, param.IsUsed);
            objectDb.AddInParameter("UserID", DbType.Int32, param.UserId);
            objectDb.AddInParameter("IP", DbType.String, param.Ip);

            AssignResultOutputParam(objectDb);
        }
    }
}
