using System;
using Fanex.Affiliate.Admin.Core.DTO.Member;
using System.Collections.Generic;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;
using Fanex.Affiliate.Admin.Core.Extensions;

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
                objectDb.AddInParameter(Field.UserName, DbType.String, userNames);
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
                objectDb.AddInParameter("deposit_usernames", DbType.String, userNames);

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
                objectDb.AddInParameter(Field.FirstName, DbType.String, param.FirstName);
                objectDb.AddInParameter(Field.LastName, DbType.String, param.LastName);
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
                objectDb.AddOutParameter(Field.UserName, DbType.String, Size.SizeOfString);

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
