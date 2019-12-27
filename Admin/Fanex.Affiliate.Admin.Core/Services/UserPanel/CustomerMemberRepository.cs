using System;
using System.Collections.Generic;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Data;
using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.Repository.UserPanel
{
    public class CustomerMemberRepository : BaseRepository, ICustomerMemberRepository
    {
        public EntityResult<CustomerMember> GetCustomerMemberList(CustomerMemberParam param)
        {
            const string SpKey = "Aff_Admin_Member_Sel";
            var result = new EntityResult<CustomerMember>();
            var dataList = new List<CustomerMember>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignParam(objectDb, param);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(new CustomerMember
                        {
                            UserName = Convert.ToString(reader["UserName"]),
                            FirstName = Convert.ToString(reader["FirstName"]),
                            LastName = Convert.ToString(reader["LastName"]),
                            Email = param.CanViewMemberEmail ? Convert.ToString(reader["Email"]) : string.Empty,
                            CurrencyName = Convert.ToString(reader["Currency"]),
                            RegisteredDate = Convert.ToDateTime(reader["RegisteredDate"])
                        });
                    }
                }

                result.MainData = dataList;

                var totalRow = objectDb.GetParameterValue("NRows");
                result.TotalItem = totalRow == DBNull.Value ? 0 : Convert.ToInt32(totalRow);
            }

            return result;
        }

        private static void AssignParam(IObjectDb objectDb, CustomerMemberParam param)
        {
            objectDb.AddInParameter("AffId", DbType.Int32, param.AffId);
            objectDb.AddInParameter("MemberType", DbType.Int16, param.MemberType);
            objectDb.AddInParameter("FromDate", DbType.Date, param.FromDate);
            objectDb.AddInParameter("ToDate", DbType.Date, param.ToDate);
            objectDb.AddInParameter("IsGetAll", DbType.Boolean, param.IsGetAll);
            objectDb.AddInParameter("PageSize", DbType.Int32, param.PageSize);
            objectDb.AddInParameter("PageIndex", DbType.Int32, param.PageIndex);
            objectDb.AddOutParameter("NRows", DbType.Int32, Size.SizeOfInt);
        }
    }
}
