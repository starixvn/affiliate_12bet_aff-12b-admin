using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Member;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;
using System;
using System.Collections.Generic;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Repository.Member
{
    public class ActiveMemberRepository : BaseRepository, IActiveMemberRepository
    {
        public AllMemberEntities GetAllMembers(string userNames)
        {
            const string SpKey = "Aff_Admin_ActiveMember_CheckStatus";

            var result = new AllMemberEntities();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue(Field.UserName, userNames);
                AssignResultOutputParam(objectDb);

                using (var reader = objectDb.ExecuteReader())
                {
                    result.ActivedMemeberList = GetActivatedMemberList(reader);
                    result.InactiveMemberList = GetInactivatedMemberList(reader);
                }

                result.MemberNotExist = objectDb.GetParameterValue("ErrMsg").ToString();
            }

            return result;
        }

        public IEnumerable<ActiveMemberInfo> GetActivatedMemberList(IDataReader reader)
        {
            var result = new List<ActiveMemberInfo>();

            while (reader.Read())
            {
                result.Add(new ActiveMemberInfo
                {
                    IbcCustId = reader.GetVal<int>("IBCCustID"),
                    IbcUserName = Convert.ToString(reader["IBCUserName"]),
                    UserName = Convert.ToString(reader[Field.UserName]),
                    FirstName = Convert.ToString(reader[Field.FirstName]),
                    LastName = Convert.ToString(reader[Field.LastName]),
                    CreatedDate = reader["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["CreatedDate"])
                });
            }

            return result;
        }

        public IEnumerable<InactiveMemberInfo> GetInactivatedMemberList(IDataReader reader)
        {
            var result = new List<InactiveMemberInfo>();

            if (reader.NextResult())
            {
                while (reader.Read())
                {
                    result.Add(new InactiveMemberInfo
                    {
                        UserName = Convert.ToString(reader[Field.UserName]),
                        FirstName = Convert.ToString(reader[Field.FirstName]),
                        LastName = Convert.ToString(reader[Field.LastName])
                    });
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
                objectDb.SetParameterValue("deposit_usernames", userNames);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new DepositCustomerInfo
                        {
                            DepositUserName = Convert.ToString(reader[Field.UserName]),
                            FirstName = Convert.ToString(reader[Field.FirstName]),
                            LastName = Convert.ToString(reader[Field.LastName]),
                            Email = Convert.ToString(reader["Email"]),
                            Password = Convert.ToString(reader["Password"]),
                            OddsType = Convert.ToString(reader["OddsType"]),
                            AffiliateId = reader.GetVal<int>("AffiliateId"),
                            Country = Convert.ToString(reader["Country"]),
                            AgentId = reader.GetVal<int>("AgentId"),
                            BonusCode = Convert.ToString(reader["BonusCode"]),
                            DepositUserId = Convert.ToInt32(reader["DepositUserId"])
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
                objectDb.SetParameterValue(Field.FirstName, param.FirstName);
                objectDb.SetParameterValue(Field.LastName, param.LastName);
                objectDb.SetParameterValue("email", param.Email);
                objectDb.SetParameterValue("depositusername", param.DepositUserName);
                objectDb.SetParameterValue("deposituserid", param.DepositUserId);
                objectDb.SetParameterValue("password", param.Password);
                objectDb.SetParameterValue("oddstype", param.OddsType);
                objectDb.SetParameterValue("agentid", param.AgentId);
                objectDb.SetParameterValue("affiliateid", param.AffiliateId);
                objectDb.SetParameterValue("bonuscode", param.BonusCode);
                objectDb.SetParameterValue("Country", param.Country);

                objectDb.SetParameterValue("custid", DefaultValue.Int);
                objectDb.SetParameterValue(Field.UserName, DefaultValue.String);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                var userId = objectDb.GetParameterValue("custid");

                param.UserId = userId == DBNull.Value ? 0 : Convert.ToInt32(userId);
                param.UserName = Convert.ToString(objectDb.GetParameterValue(Field.UserName));

                return GetResultInfo(objectDb);
            }
        }

        public void WriteLogAfterAdd12BetPlayerAccountToIBCSuccefully(ActiveMemberParam param, int depositUserId)
        {
            const string SpKey = "Aff_WriteLog";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("AffID", param.UserId);
                objectDb.SetParameterValue("Creator", param.UserId);
                objectDb.SetParameterValue("IP", param.Ip);
                objectDb.SetParameterValue("Typename", "Member");
                objectDb.SetParameterValue("SPName", "Dot_Dep_CustomerInsMember_Ag_Cust");
                objectDb.SetParameterValue("Details", $"Add 12Bet player account to IBC DepositCustID={depositUserId}");

                objectDb.SetParameterValue("RefNo", DefaultValue.Int);

                objectDb.ExecuteNonQuery();
            }
        }

        public ResultInfo Update12BetPlayerStatusToDeposit(string depositUserName, int ibcCustid, string ibcUserName)
        {
            const string SpKey = "Deposit_MemberIBCDetail";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("depusername", depositUserName);
                objectDb.SetParameterValue("ibccustid", ibcCustid);
                objectDb.SetParameterValue("ibcusername", ibcUserName);

                objectDb.SetParameterValue("ErrorCode", DefaultValue.Int);
                objectDb.SetParameterValue("ErrorMsg", DefaultValue.String);

                objectDb.ExecuteNonQuery();

                var code = objectDb.GetParameterValue("ErrorCode");

                return new ResultInfo
                {
                    Code = code == DBNull.Value ? 0 : Convert.ToInt32(code),
                    Message = objectDb.GetParameterValue("ErrorMsg")?.ToString()
                };
            }
        }

        public void WriteLogAfterUpdate12BetPlayerStatusToDepositSuccefully(ActiveMemberParam param, int ibcCustId)
        {
            const string SpKey = "Aff_WriteLog";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("AffID", param.UserId);
                objectDb.SetParameterValue("Creator", param.UserId);
                objectDb.SetParameterValue("IP", param.Ip);
                objectDb.SetParameterValue("Typename", "Member");
                objectDb.SetParameterValue("SPName", "Deposit_MemberIBCDetail");
                objectDb.SetParameterValue("Details", $"Update 12Bet player status to deposit DepositCustID={ibcCustId}");

                objectDb.SetParameterValue("RefNo", DefaultValue.Int);

                objectDb.ExecuteNonQuery();
            }
        }
    }
}
