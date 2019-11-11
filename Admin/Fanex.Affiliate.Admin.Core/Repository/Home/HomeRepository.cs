using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.Home;
using Fanex.Affiliate.Admin.Core.DTO;
using System.Data;
using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.Repository.Home
{
    public class HomeRepository : BaseRepository, IHomeRepository
    {
        public EntityResult<OnlineListInfo> GetOnlineList(int pageIndex, int pageSize)
        {
            const string SpKey = "Aff_UserOnlineList";

            var result = new EntityResult<OnlineListInfo>();
            var dataList = new List<OnlineListInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("PageIndex", DbType.Int32, pageIndex);
                objectDb.AddInParameter("PageSize", DbType.Int32, pageSize);

                objectDb.AddOutParameter("TotalPages", DbType.Int32, Size.SizeOfInt);
                objectDb.AddOutParameter("NRows", DbType.Int32, Size.SizeOfInt);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(new OnlineListInfo
                        {
                            UserName = Convert.ToString(reader["UserName"]),
                            LoggedInDate = Convert.ToDateTime(reader["LoggedIn"]),
                            LastRequestDate = Convert.ToDateTime(reader["LastRequest"]),
                            IP = Convert.ToString(reader["IP"]),
                            Role = Convert.ToString(reader["Role"])
                        });
                    }
                }

                result.MainData = dataList;
                var totalRow = objectDb.GetParameterValue("NRows");
                result.TotalItem = totalRow == DBNull.Value ? 0 : Convert.ToInt32(totalRow);
            }

            return result;
        }
    }
}
