using System;
using Fanex.Affiliate.Admin.Core.DTO.Member;
using System.Collections.Generic;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Repository.Member
{
    public class ActiveMemberRepository : BaseRepository, IActiveMemberRepository
    {
        public IEnumerable<UnactiveMemberInfo> GetUnactivedMemberList(string userNames)
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

        public IEnumerable<ActiveMemberInfo> GetActivedMemberList(string userNames)
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
    }
}
