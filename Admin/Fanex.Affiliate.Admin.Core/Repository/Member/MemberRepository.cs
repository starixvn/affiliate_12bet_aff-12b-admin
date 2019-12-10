using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Member;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;
using System.Data;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using DNAide.Core;

namespace Fanex.Affiliate.Admin.Core.Repository.Member
{
    public class MemberRepository : BaseRepository, IMemberRepository
    {
        public EntityResult<MemberInfo> GetMemberList(MemberParam param)
        {
            const string Spkey = "Aff_MemberListSel";
            var result = new EntityResult<MemberInfo>();
            var dataList = new List<MemberInfo>();

            using (var objectDb = GetObjectDb(Spkey))
            {
                objectDb.AddInParameter("AffUsername", DbType.String, param.AffName);
                objectDb.AddInParameter("UserName", DbType.String, param.UserName);
                objectDb.AddInParameter("IBCUserName", DbType.String, param.IbcUserName);
                objectDb.AddInParameter("PageIndex", DbType.Int32, param.PageIndex);
                objectDb.AddInParameter("PageSize", DbType.Int32, param.PageSize);

                objectDb.AddOutParameter("NRows", DbType.Int32, Size.SizeOfInt);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(new MemberInfo {
                            MemberId = Convert.ToInt32(reader["IBCCustID"]),
                            UserName = Convert.ToString(reader["UserName"]),
                            IbcUserName = Convert.ToString(reader["IBCUserName"]),
                            FirstName =  Convert.ToString(reader["FirstName"]),
                            LastName = Convert.ToString(reader["LastName"]),
                            CreatedDate = reader["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["CreatedDate"]),
                            AffName = Convert.ToString(reader["AffUserName"]),
                            CurrentBalance = FormatNumber(reader["CurrBalance"])
                        });
                    }
                }

                result.MainData = dataList;
                var totalRow = objectDb.GetParameterValue("NRows");
                result.TotalItem = totalRow == DBNull.Value ? 0 : Convert.ToInt32(totalRow);
            }

            return result;
        }

        public ResultInfo DeleteMember(DeleteMemberParam param)
        {
            const string SpKey = "Aff_MemberDel";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("ibccustid", DbType.Int32, param.MemberId);
                objectDb.AddInParameter("UserID", DbType.Int32, param.UserId);
                objectDb.AddInParameter("IP", DbType.String, param.Ip);

                objectDb.AddOutParameter("AffID", DbType.Int32, Size.SizeOfInt);
                objectDb.AddOutParameter("RefNo", DbType.Int64, Size.SizeOfInt);
                objectDb.AddOutParameter("DepositCustID", DbType.Int32, Size.SizeOfInt);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                var affId = objectDb.GetParameterValue("AffID");
                var depositMemberId = objectDb.GetParameterValue("DepositCustID");

                param.AffId = affId == DBNull.Value ? 0 : Convert.ToInt32(affId);
                param.DepositMemberId = depositMemberId == DBNull.Value ? 0 : Convert.ToInt32(depositMemberId);

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo DeleteMemberInDepositDB(int depositMemberId)
        {
            const string SpKey = "DEP_AdminUpdAffID";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("DepositCustID", DbType.Int32, depositMemberId);
                AssignResultOutputParam(objectDb);
                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public void WriteAfterDeleteSuccefully(DeleteMemberParam param)
        {
            const string SpKey = "Aff_WriteLog";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("AffID", DbType.Int32, param.AffId);
                objectDb.AddInParameter("Creator", DbType.Int32, param.UserId);
                objectDb.AddInParameter("IP", DbType.String, param.Ip);
                objectDb.AddInParameter("Typename", DbType.String, "Member");
                objectDb.AddInParameter("SPName", DbType.String, "DEP_AdminUpdAffID");
                objectDb.AddInParameter("Details", DbType.String, $"Remove member. DepositCustID={param.DepositMemberId} (On Deposit_DB)");

                objectDb.AddOutParameter("RefNo", DbType.Int64, Size.SizeOfInt);

                objectDb.ExecuteNonQuery();
            }
        }

        private static string FormatNumber(object Number)
        {
            decimal decimalNumber = CastAide.AsDecimal(Number, 0m);

            return (decimalNumber >= 0 ? "$" : "-$") + Math.Abs(decimalNumber).ToString("N2");

        }
    }
}
