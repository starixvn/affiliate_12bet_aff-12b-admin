using System;
using Fanex.Affiliate.Admin.Core.MetaModels.EmailHistory;
using Fanex.Affiliate.Admin.Core.DTO.EmailHistory;
using Fanex.Affiliate.Admin.Core.DTO;
using System.Collections.Generic;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Repository.EmailHistory
{
    class EmailHistoryRepository : BaseRepository, IEmailHistoryRepository
    {
        public ResultInfo DeleteEmailHistory(DeleteEmailHistoryParam param)
        {
            const string SpKey = "Aff_EmailHistoryDel";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("listmid", DbType.String, param.EmailIdList);
                objectDb.AddInParameter("userid", DbType.Int32, param.UserId);
                objectDb.AddInParameter("ip", DbType.String, param.Ip);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public EmailHistoryInfo GetEmailHistoryDetail(int selectedEmailId)
        {
            const string SpKey = "Aff_EmailHistorySel";

            EmailHistoryInfo result = null;

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("MID", DbType.Int32, selectedEmailId);

                using (var reader = objectDb.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new EmailHistoryInfo
                        {
                            EmailId = Convert.ToInt32(reader["MID"]),
                            Content = Convert.ToString(reader["Content"]),
                            Sender = Convert.ToString(reader["Sender"]),
                            Email = Convert.ToString(reader["Email"]),
                            Subject = Convert.ToString(reader["Subject"]),
                            AffName = Convert.ToString(reader["affname"]),
                            SentDate = Convert.ToDateTime(reader["CreateDate"]),
                        };
                    }
                }
            }

            return result;
        }

        public EntityResult<EmailHistoryInfo> GetEmailHistoryList(EmailHistoryParam param)
        {
            const string SpKey = "Aff_EmailHistorySel";
            var result = new EntityResult<EmailHistoryInfo>();
            var dataList = new List<EmailHistoryInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                if (param.HasGetByDate)
                {
                    objectDb.AddInParameter("FromDate", DbType.DateTime, param.FromDate);
                    objectDb.AddInParameter("ToDate", DbType.DateTime, param.ToDate);
                }
                objectDb.AddInParameter("MID", DbType.Int32, param.EmailId);
                objectDb.AddInParameter("SenderName", DbType.String, param.SenderName);
                objectDb.AddInParameter("Email", DbType.String, param.Email);
                objectDb.AddInParameter("AffiliateUserName", DbType.String, param.AffName);
                objectDb.AddInParameter("PageIndex", DbType.Int32, param.PageIndex);
                objectDb.AddInParameter("PageSize", DbType.Int32, param.PageSize);

                objectDb.AddOutParameter("NRows", DbType.Int32, 4);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(new EmailHistoryInfo
                        {
                            EmailId = Convert.ToInt32(reader["MID"]),
                            Content = Convert.ToString(reader["Content"]),
                            Sender = Convert.ToString(reader["Sender"]),
                            Email = Convert.ToString(reader["Email"]),
                            Subject = Convert.ToString(reader["Subject"]),
                            AffName = Convert.ToString(reader["affname"]),
                            SentDate = Convert.ToDateTime(reader["CreateDate"]),
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
