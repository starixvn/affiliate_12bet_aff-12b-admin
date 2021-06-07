using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Member;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;
using Fanex.Affiliate.Admin.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Services.Member
{
    public class RegisteredMemberService : BaseRepository, IRegisteredMemberService
    {
        public EntityResult<RegisteredMemberInfo> GetRegisteredMemberList(MemberParam param)
        {
            const string Spkey = "Aff_Admin_RegisteredMember_Sel";
            var result = new EntityResult<RegisteredMemberInfo>();
            var dataList = new List<RegisteredMemberInfo>();

            using (var objectDb = GetObjectDb(Spkey))
            {
                objectDb.AddInParameter("AffName", DbType.String, param.AffName);
                objectDb.AddInParameter("UserName", DbType.String, param.UserName);
                objectDb.AddInParameter("PageIndex", DbType.Int32, param.PageIndex);
                objectDb.AddInParameter("PageSize", DbType.Int32, param.PageSize);

                objectDb.AddOutParameter("NRows", DbType.Int32, Size.SizeOfInt);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(new RegisteredMemberInfo
                        {
                            UserName = Convert.ToString(reader["UserName"]),
                            FirstName = Convert.ToString(reader["FirstName"]),
                            LastName = Convert.ToString(reader["LastName"]),
                            CreatedDate = reader["CreatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["CreatedDate"]),
                            AffName = Convert.ToString(reader["AffName"]),
                            Currency = Convert.ToString(reader["Currency"])
                        });
                    }
                }

                result.MainData = dataList;
                result.TotalItem = GetTotalRow(objectDb);
            }

            return result;
        }
    }
}
