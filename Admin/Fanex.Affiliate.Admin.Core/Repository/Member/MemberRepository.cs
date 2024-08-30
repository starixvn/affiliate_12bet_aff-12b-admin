using DNAide.Core;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Member;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;
using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Repository.Member
{
    public class MemberRepository : BaseRepository, IMemberRepository
    {
        public EntityResult<MemberInfo> GetMemberList(MemberParam param)
        {
            const string Spkey = "Aff_Admin_MemberList_Sel";
            var result = new EntityResult<MemberInfo>();
            var dataList = new List<MemberInfo>();

            using (var objectDb = GetObjectDb(Spkey))
            {
                objectDb.SetParameterValue("AffUsername", param.AffName);
                objectDb.SetParameterValue("UserName", param.UserName);
                objectDb.SetParameterValue("IBCUserName", param.IbcUserName);
                objectDb.SetParameterValue("PageIndex", param.PageIndex);
                objectDb.SetParameterValue("PageSize", param.PageSize);

                objectDb.SetParameterValue("NRows", DefaultValue.Int);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(new MemberInfo
                        {
                            MemberId = Convert.ToInt32(reader["IBCCustID"]),
                            UserName = Convert.ToString(reader["UserName"]),
                            IbcUserName = Convert.ToString(reader["IBCUserName"]),
                            FirstName = Convert.ToString(reader["FirstName"]),
                            LastName = Convert.ToString(reader["LastName"]),
                            CreatedDate = reader["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["CreatedDate"]),
                            AffName = Convert.ToString(reader["AffUserName"]),
                            IsRemoved = Convert.ToBoolean(reader["IsRemoved"]),
                            CurrentBalance = FormatNumber(reader["CurrBalance"])
                        });
                    }
                }

                result.MainData = dataList;
                result.TotalItem = GetTotalRow(objectDb);
            }

            return result;
        }

        public ResultInfo UpdateMemberStatus(UpdateMemberStatusParam param)
        {
            const string SpKey = "Aff_Admin_Member_RemoveStatus_Upd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("IsRemoved", param.IsRemoved);
                objectDb.SetParameterValue("ibccustid", param.MemberId);
                objectDb.SetParameterValue("UserID", param.UserId);
                objectDb.SetParameterValue("IP", param.Ip);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        private static string FormatNumber(object Number)
        {
            var decimalString = CastAide.AsDecimal(Number, 0m).ToString("N2");
            var decimalNumber = Convert.ToDecimal(decimalString);

            return (decimalNumber >= 0 ? "$" : "-$") + Math.Abs(decimalNumber);

        }
    }
}
