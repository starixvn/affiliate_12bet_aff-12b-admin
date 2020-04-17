using Fanex.Affiliate.Admin.Core.DTO.EmailHistory;
using Fanex.Affiliate.Admin.Core.MetaModels.EmailHistory;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.DTO;
using System.Data;
using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.Services.EmailHistory
{
    public class EmailHistoryService : BaseRepository, IEmailHistoryService
    {
        public ResultInfo DeleteEmailHistory(DeleteEmailHistoryParam param)
        {
            const string SpKey = "Aff_Admin_EmailHistory_Del";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("MIDs", DbType.String, param.EmailIdList);
                objectDb.AddInParameter("userid", DbType.Int32, param.UserId);
                objectDb.AddInParameter("ip", DbType.String, param.Ip);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public EmailHistoryDetailInfo GetEmailHistoryDetail(int selectedEmailId)
        {
            const string SpKey = "Aff_Admin_EmailHistory_Detail_Sel";

            EmailHistoryDetailInfo result = null;

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("MID", DbType.Int32, selectedEmailId);

                using (var reader = objectDb.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = BuildDataItem(reader);
                        result.Content = Convert.ToString(reader["Content"]);
                    }
                }
            }

            return result;
        }

        public EntityResult<EmailHistoryInfo> GetEmailHistoryList(EmailHistoryParam param)
        {
            const string SpKey = "Aff_Admin_EmailHistory_Sel";
            var result = new EntityResult<EmailHistoryInfo>();
            var dataList = new List<EmailHistoryInfo>();
            EmailHistoryInfo dataItem = null;

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("SearchById", DbType.Int32, param.SearchById);
                objectDb.AddInParameter("IsGetAll", DbType.Boolean, param.IsGetAll);
                objectDb.AddInParameter("SearchText", DbType.String, param.SearchText);
                objectDb.AddInParameter("FromDate", DbType.DateTime, param.FromDate);
                objectDb.AddInParameter("ToDate", DbType.DateTime, param.ToDate);
                objectDb.AddInParameter("PageIndex", DbType.Int32, param.PageIndex);
                objectDb.AddInParameter("PageSize", DbType.Int32, param.PageSize);

                objectDb.AddOutParameter("NRows", DbType.Int32, Size.SizeOfInt);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataItem = BuildDataItem(reader);
                        dataItem.AffName = Convert.ToString(reader["Affname"]);

                        dataList.Add(dataItem);
                    }
                }

                result.MainData = dataList;
                var totalRow = objectDb.GetParameterValue("NRows");
                result.TotalItem = totalRow == DBNull.Value ? 0 : Convert.ToInt32(totalRow);
            }

            return result;
        }

        private static EmailHistoryDetailInfo BuildDataItem(IDataReader reader)
        {
            return new EmailHistoryDetailInfo
            {
                EmailId = Convert.ToInt32(reader["MID"]),
                Sender = Convert.ToString(reader["Sender"]),
                Email = Convert.ToString(reader["Email"]),
                Subject = Convert.ToString(reader["Subject"]),
                SentDate = Convert.ToDateTime(reader["CreatedDate"]),
            };
        }
    }
}
