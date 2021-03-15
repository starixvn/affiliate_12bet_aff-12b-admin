using DNAide.Core;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Member;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;
using System;
using System.Collections.Generic;
using System.Data;

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
                var totalRow = objectDb.GetParameterValue("NRows");
                result.TotalItem = totalRow == DBNull.Value ? 0 : Convert.ToInt32(totalRow);
            }

            return result;
        }

        public ResultInfo UpdateMemberStatus(UpdateMemberStatusParam param)
        {
            const string SpKey = "Aff_Admin_Member_RemoveStatus_Upd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("IsRemoved", DbType.Int32, param.IsRemoved);
                objectDb.AddInParameter("ibccustid", DbType.Int32, param.MemberId);
                objectDb.AddInParameter("UserID", DbType.Int32, param.UserId);
                objectDb.AddInParameter("IP", DbType.String, param.Ip);

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
