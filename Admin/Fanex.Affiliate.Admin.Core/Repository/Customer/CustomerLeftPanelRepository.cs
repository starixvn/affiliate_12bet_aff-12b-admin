using System;
using System.Collections.Generic;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Customer;
using Sunplus.Agent.Configurations;
using Fanex.Affiliate.Admin.Core.MetaModels.Customer;

namespace Fanex.Affiliate.Admin.Core.Repository.Customer
{
    public class CustomerLeftPanelRepository : BaseRepository, ICustomerLeftPanelRepository
    {
        public AffiliateInfoOnLeftPanel GetAffiliateInfoOnLeftPanel(string affUserName)
        {
            const string SpKey = "Aff_Admin_AffiliateInfo_Sel";
            AffiliateInfoOnLeftPanel result = null;

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("Username", DbType.String, affUserName);

                using (var reader = objectDb.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new AffiliateInfoOnLeftPanel
                        {
                            AffId = Convert.ToInt32(reader["AffId"]),
                            GroupId = Convert.ToInt32(reader["GroupId"]),
                            AffName = Convert.ToString(reader["UserName"]),
                            FirstName = Convert.ToString(reader["FirstName"]),
                            LastName = Convert.ToString(reader["LastName"]),
                            PreferredLanguage = Convert.ToString(reader["PreferredLanguage"]),
                            AffType = Convert.ToByte(reader["AffType"]),
                            BalanceBf = Convert.ToDecimal(reader["BalBroughtForward"]),
                            CommGroupName = Convert.ToString(reader["GroupName"]),
                            ContactApps = Convert.ToString(reader["ContactApps"]),
                            Gender = Convert.ToBoolean(reader["Gender"]),
                            IsClosed = Convert.ToBoolean(reader["Closed"]),
                            LastLoginDate = reader["LastLoginDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["LastLoginDate"]),
                            LastLoginIp = Convert.ToString(reader["LastLoginIp"]),
                            MoreSiteUrl = Convert.ToString(reader["MoreSiteUrls"]),
                            SiteUrl = Convert.ToString(reader["SiteUrl"]),
                            NoSiteDescription = Convert.ToString(reader["NoSiteDesc"]),
                            Status = Convert.ToString(reader["Status"]),
                            Phone = Convert.ToString(reader["Phone"])
                        };
                    }
                }
            }

            return result;
        }

        public IEnumerable<OnlineList> GetOnlineList(string affUserName)
        {
            const string SpKey = "Aff_Admin_LeftPanelOnlineList_Sel";
            var result = new List<OnlineList>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("Username", DbType.String, affUserName);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new OnlineList
                        {
                            Ip = Convert.ToString(reader["Ip"]),
                            ReloadTime = Convert.ToDateTime(reader["ReloadTime"])
                        });
                    }
                }
            }

            return result;
        }

        public ResultInfo UpdateAffType(UpdateAffTypeParam param)
        {
            const string SpKey = "Aff_UpdAffType";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("AffID", DbType.Int32, param.AffId);
                objectDb.AddInParameter("UserId", DbType.Int32, param.UserId);
                objectDb.AddInParameter("AffType", DbType.Byte, param.AffType);
                objectDb.AddInParameter("IP", DbType.String, param.Ip);
                objectDb.AddInParameter("Language", DbType.String, param.Language);

                objectDb.AddOutParameter("ErrCode", DbType.Int32, 4);

                objectDb.ExecuteNonQuery();

                var code = objectDb.GetParameterValue("ErrCode");

                return new ResultInfo
                {
                    Code = code == DBNull.Value ? 0 : Convert.ToInt32(code),
                };
            }
        }

        public ResultInfo UpdateClosedStatus(UpdateClosedStatusParam param)
        {
            const string SpKey = "Aff_AffiliateUpdClosedStatus";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("AffId", DbType.Int32, param.AffId);
                objectDb.AddInParameter("UserId", DbType.Int32, param.UserId);
                objectDb.AddInParameter("Closed", DbType.Boolean, param.IsClosed);
                objectDb.AddInParameter("IP", DbType.String, param.Ip);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo UpdateLanguage(UpdateLanguageParam param)
        {
            const string SpKey = "Aff_PreferLangUpd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("AffId", DbType.Int32, param.AffId);
                objectDb.AddInParameter("UserId", DbType.Int32, param.UserId);
                objectDb.AddInParameter("Ip", DbType.String, param.Ip);
                objectDb.AddInParameter("PreferredLanguage", DbType.String, param.PreferredLanguage);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo UpdateStatus(UpdateStatusParam param)
        {
            const string SpKey = "Aff_AffiliateUpdStatus";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("AffId", DbType.Int32, param.AffId);
                objectDb.AddInParameter("UserId", DbType.Int32, param.UserId);
                objectDb.AddInParameter("Ip", DbType.String, param.Ip);
                objectDb.AddInParameter("Status", DbType.String, param.Status);
                if (param.GroupId != -1)
                {
                    objectDb.AddInParameter("GroupId", DbType.Int32, param.GroupId);
                }

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }
    }
}
