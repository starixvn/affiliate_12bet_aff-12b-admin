using System;
using Fanex.Affiliate.Admin.Core.DTO.Member;
using System.Collections.Generic;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;

namespace Fanex.Affiliate.Admin.Core.Repository.Member
{
    public class ActiveMemberRepository : BaseRepository, IActiveMemberRepository
    {
        public IEnumerable<UnactiveMemberInfo> GetUnactivatedMemberList(string userNames)
        {
            const string Spkey = "Deposit_MembersNotActivatedSel";
            var result = new List<UnactiveMemberInfo>();

            using (var objectDb = GetObjectDb(Spkey))
            {
                objectDb.AddInParameter("UserNames", DbType.String, userNames);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new UnactiveMemberInfo
                        {
                            UserName = Convert.ToString(reader["UserName"]),
                            FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                            LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"])
                        });
                    }
                }
            }

            return result;
        }

        public IEnumerable<ActiveMemberInfo> GetActivatedMemberList(string userNames)
        {
            const string Spkey = "Aff_CustomerCheckList12BetPlayerAccountsActivated";
            var result = new List<ActiveMemberInfo>();

            using (var objectDb = GetObjectDb(Spkey))
            {
                objectDb.AddInParameter("UserNames", DbType.String, userNames);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new ActiveMemberInfo
                        {
                            IbcCustId = Convert.ToInt32(reader["IBCCustID"]),
                            IbcUserName = Convert.ToString(reader["IBCUserName"]),
                            UserName = Convert.ToString(reader["UserName"]),
                            FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                            LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                            CreatedDate = reader["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["CreatedDate"])
                        });
                    }
                }
            }

            return result;
        }

        public IEnumerable<DepositCustomerInfo> Get12BetPlayersDepositInfo(string userNames)
        {
            const string Spkey = "Deposit_GetCustDataForCreateIBCACByUserNames";
            var result = new List<DepositCustomerInfo>();

            using (var objectDb = GetObjectDb(Spkey))
            {
                objectDb.AddInParameter("deposit_usernames", DbType.String, userNames);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new DepositCustomerInfo
                        {
                            DepositUserName = Convert.ToString(reader["UserName"]),
                            FirstName = reader["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FirstName"]),
                            LastName = reader["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LastName"]),
                            Email = Convert.ToString(reader["Email"]),
                            Password = Convert.ToString(reader["Password"]),
                            OddsType = reader["OddsType"] == DBNull.Value ? string.Empty : Convert.ToString(reader["OddsType"]),
                            AffiliateId = reader["AffiliateId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["AffiliateId"]),
                            Country = reader["Country"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Country"]),
                            AgentId = reader["AgentId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["AgentId"]),
                            BonusCode = reader["BonusCode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["BonusCode"]),
                            DepositUserId = Convert.ToInt32(reader["DepositUserId"]),
                        });
                    }
                }
            }

            return result;
        }

        public ResultInfo Add12BetPlayerAccountToIBC(DepositCustomerInfo param)
        {
            const string SpKey = "Dot_Dep_CustomerInsMember_Ag_Cust";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("firstname", DbType.String, param.FirstName);
                objectDb.AddInParameter("lastname", DbType.String, param.LastName);
                objectDb.AddInParameter("email", DbType.String, param.Email);
                objectDb.AddInParameter("depositusername", DbType.String, param.DepositUserName);
                objectDb.AddInParameter("deposituserid", DbType.String, param.DepositUserId);
                objectDb.AddInParameter("password", DbType.String, param.Password);
                objectDb.AddInParameter("oddstype", DbType.String, param.OddsType);
                objectDb.AddInParameter("agentid", DbType.String, param.AgentId);
                objectDb.AddInParameter("affiliateid", DbType.String, param.AffiliateId);
                objectDb.AddInParameter("bonuscode", DbType.String, param.BonusCode);
                objectDb.AddInParameter("Country", DbType.String, param.Country);

                objectDb.AddOutParameter("custid", DbType.Int32, Size.SizeOfInt);
                objectDb.AddOutParameter("username", DbType.String, Size.SizeOfString);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                var userId = objectDb.GetParameterValue("custid");
                var userName = objectDb.GetParameterValue("username");

                param.UserId = userId == DBNull.Value ? 0 : Convert.ToInt32(userId);
                param.UserName = userName == DBNull.Value ? string.Empty : Convert.ToString(userName);

                return GetResultInfo(objectDb);
            }
        }

        public void WriteLogAfterAdd12BetPlayerAccountToIBCSuccefully(ActiveMemberParam param, int depositUserId)
        {
            const string SpKey = "Aff_WriteLog";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("AffID", DbType.Int32, param.UserId);
                objectDb.AddInParameter("Creator", DbType.Int32, param.UserId);
                objectDb.AddInParameter("IP", DbType.String, param.Ip);
                objectDb.AddInParameter("Typename", DbType.String, "Member");
                objectDb.AddInParameter("SPName", DbType.String, "Dot_Dep_CustomerInsMember_Ag_Cust");
                objectDb.AddInParameter("Details", DbType.String, $"Add 12Bet player account to IBC DepositCustID={depositUserId}");

                objectDb.AddOutParameter("RefNo", DbType.Int64, Size.SizeOfInt);

                objectDb.ExecuteNonQuery();
            }
        }

        public ResultInfo Update12BetPlayerStatusToDeposit(string depositUserName, int ibcCustid, string ibcUserName)
        {
            const string SpKey = "Deposit_MemberIBCDetail";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("depusername", DbType.String, depositUserName);
                objectDb.AddInParameter("ibccustid", DbType.String, ibcCustid);
                objectDb.AddInParameter("ibcusername", DbType.String, ibcUserName);

                objectDb.AddOutParameter("ErrorCode", DbType.Int32, Size.SizeOfInt);
                objectDb.AddOutParameter("ErrorMsg", DbType.String, Size.SizeOfString);

                objectDb.ExecuteNonQuery();

                var code = objectDb.GetParameterValue("ErrorCode");

                return new ResultInfo
                {
                    Code = code == DBNull.Value ? 0 : Convert.ToInt32(code),
                    Message = objectDb.GetParameterValue("ErrorMsg").ToString()
                };
            }
        }

        public void WriteLogAfterUpdate12BetPlayerStatusToDepositSuccefully(ActiveMemberParam param, int ibcCustId)
        {
            const string SpKey = "Aff_WriteLog";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("AffID", DbType.Int32, param.UserId);
                objectDb.AddInParameter("Creator", DbType.Int32, param.UserId);
                objectDb.AddInParameter("IP", DbType.String, param.Ip);
                objectDb.AddInParameter("Typename", DbType.String, "Member");
                objectDb.AddInParameter("SPName", DbType.String, "Deposit_MemberIBCDetail");
                objectDb.AddInParameter("Details", DbType.String, $"Update 12Bet player status to deposit DepositCustID={ibcCustId}");

                objectDb.AddOutParameter("RefNo", DbType.Int64, Size.SizeOfInt);

                objectDb.ExecuteNonQuery();
            }
        }
    }
}
