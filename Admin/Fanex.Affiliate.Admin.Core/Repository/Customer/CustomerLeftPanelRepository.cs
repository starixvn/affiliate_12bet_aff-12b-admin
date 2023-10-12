using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Customer;
using Fanex.Affiliate.Admin.Core.MetaModels.Customer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

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
                objectDb.SetParameterValue("Username", affUserName);

                using (var reader = objectDb.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new AffiliateInfoOnLeftPanel
                        {
                            AffId = Convert.ToInt32(reader[Field.AffId]),
                            GroupId = Convert.ToInt32(reader["GroupId"]),
                            AffName = Convert.ToString(reader["UserName"]),
                            FirstName = HttpUtility.HtmlDecode(Convert.ToString(reader["FirstName"])),
                            LastName = HttpUtility.HtmlDecode(Convert.ToString(reader["LastName"])),
                            TargetCountry = Convert.ToString(reader["PreferredLanguage"]),
                            PreferredLanguage = Convert.ToString(reader["PreferredLanguage"]),
                            AffType = Convert.ToByte(reader["AffType"]),
                            BalanceBf = Convert.ToDecimal(reader["BalBroughtForward"]),
                            CommGroupName = Convert.ToString(reader["GroupName"]),
                            ContactApps = HttpUtility.HtmlDecode(Convert.ToString(reader["ContactApps"])),
                            Gender = Convert.ToBoolean(reader["Gender"]),
                            IsClosed = Convert.ToBoolean(reader["Closed"]),
                            LastLoginDate = reader["LastLoginDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["LastLoginDate"]),
                            LastLoginIp = Convert.ToString(reader["LastLoginIp"]),
                            MoreSiteUrl = Convert.ToString(reader["MoreSiteUrls"]),
                            SiteUrl = Convert.ToString(reader["SiteUrl"]),
                            NoSiteDescription = Convert.ToString(reader["NoSiteDesc"]),
                            Status = Convert.ToString(reader["Status"]),
                            Phone = HttpUtility.HtmlDecode(Convert.ToString(reader["Phone"]))
                        };
                    }
                }
            }

            return result;
        }

        public IEnumerable<OnlineList> GetOnlineList(string affUserName)
        {
            const string SpKey = "Aff_Admin_AffiliateOnlineList_Sel";
            var result = new List<OnlineList>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("Username", affUserName);

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
            const string SpKey = "Aff_Admin_AffiliateAffType_Upd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue(Field.AffId, param.AffId);
                objectDb.SetParameterValue(Field.UserId, param.UserId);
                objectDb.SetParameterValue("AffType", param.AffType);
                objectDb.SetParameterValue("IP", param.Ip);

                objectDb.SetParameterValue("ErrCode", DefaultValue.Int);

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
            const string SpKey = "Aff_Admin_AffiliateClosed_Upd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue(Field.AffId, param.AffId);
                objectDb.SetParameterValue(Field.UserId, param.UserId);
                objectDb.SetParameterValue("IsClosed", param.IsClosed);
                objectDb.SetParameterValue("IP", param.Ip);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo UpdateLanguage(UpdateLanguageParam param)
        {
            const string SpKey = "Aff_Admin_AffiliatePreferredLanguage_Upd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue(Field.AffId, param.AffId);
                objectDb.SetParameterValue(Field.UserId, param.UserId);
                objectDb.SetParameterValue("Ip", param.Ip);
                objectDb.SetParameterValue("PreferredLanguage", param.PreferredLanguage);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo UpdateContactNumber(UpdateContactNumberParam param)
        {
            const string SpKey = "Aff_Admin_AffiliatePhoneNumber_Upd";

            using (var objectDb = GetObjectDb(SpKey))
            {

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery(param);

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo UpdateStatus(UpdateStatusParam param)
        {
            const string SpKey = "Aff_Admin_AffiliateStatus_Upd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue(Field.AffId, param.AffId);
                objectDb.SetParameterValue(Field.UserId, param.UserId);
                objectDb.SetParameterValue("Ip", param.Ip);
                objectDb.SetParameterValue("Status", param.Status);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }


    }
}
