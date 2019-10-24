using System;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using System.Collections.Generic;
using System.Data;
using Fanex.Data;
using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.Repository.Domain
{
    public class DomainRepository : BaseRepository, IDomainRepository
    {
        public IEnumerable<DomainInfo> GetDomainList()
        {
            const string SpKey = "Aff_DomainAffSel";
            var result = new List<DomainInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(BuildDomainInfoItem(reader));
                    }
                }
            }

            return result;
        }

        public ResultInfo InsertDomain(InsertUpdateDomainParam param)
        {
            const string SpKey = "Aff_DomainAffIns";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddOutParameter("DomainId", DbType.Int32, Size.SizeOfInt);

                AssignParam(objectDb, param);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo UpdateDomain(InsertUpdateDomainParam param)
        {
            const string SpKey = "Aff_DomainAffUpd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("DomainId", DbType.Int32, param.DomainId);
                AssignParam(objectDb, param);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo DeleteDomain(DeleteDomainParam param)
        {
            const string SpKey = "Aff_DomainAffDel";

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

        private static void AssignParam(IObjectDb objectDb, InsertUpdateDomainParam param)
        {
            objectDb.AddInParameter("DomainName", DbType.String, param.DomainName);
            objectDb.AddInParameter("IsSSL", DbType.Boolean, param.IsSSL);
            objectDb.AddInParameter("UserID", DbType.Int32, param.UserId);
            objectDb.AddInParameter("IP", DbType.String, param.Ip);

            AssignResultOutputParam(objectDb);
        }

        private static DomainInfo BuildDomainInfoItem(IDataReader reader)
        {
            return new DomainInfo
            {
                DomainId = Convert.ToInt32(reader["DomainId"]),
                DomainName = Convert.ToString(reader["DomainName"]),
                IsSSL = Convert.ToBoolean(reader["IsSSL"]),
                Creator = Convert.ToString(reader["Creator"]),
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
            };
        }
    }
}
