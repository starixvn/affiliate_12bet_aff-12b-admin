using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.FeedbackReport;
using Fanex.Affiliate.Admin.Core.MetaModels.FeedbackReport;
using Fanex.Affiliate.Admin.Core.Repository;
using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.FeedbackReport
{
    public class FeedbackReportService : BaseRepository, IFeedbackReportService
    {
        public EntityResult<FeedbackReportInfo> GetFeedbackReportList(FeedbackReportParam param)
        {
            const string SpKey = "Aff_Admin_Feedback_Sel";
            var result = new EntityResult<FeedbackReportInfo>();
            var dataList = new List<FeedbackReportInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("AffName", param.AffName);
                objectDb.SetParameterValue("RateIds", param.RateIds);
                objectDb.SetParameterValue("IsExportExcel", param.IsExportExcel);
                objectDb.SetParameterValue("FromDate", param.FromDate);
                objectDb.SetParameterValue("ToDate", param.ToDate);
                objectDb.SetParameterValue("PageIndex", param.PageIndex);
                objectDb.SetParameterValue("PageSize", param.PageSize);

                objectDb.SetParameterValue("NRows", DefaultValue.Int);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(new FeedbackReportInfo {
                            AffName = Convert.ToString(reader["UserName"]),
                            RateName = Convert.ToString(reader["RateName"]),
                            Message = Convert.ToString(reader["Message"]),
                            SentDate = Convert.ToDateTime(reader["SentDate"]),
                        });
                    }
                }

                result.MainData = dataList;
                result.TotalItem = GetTotalRow(objectDb);
            }

            return result;
        }

        public IEnumerable<RateInfo> GetRateList()
        {
            const string SpKey = "Aff_RateSel";
            var dataList = new List<RateInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(new RateInfo
                        {
                            RateId = Convert.ToByte(reader["RateId"]),
                            RateName = Convert.ToString(reader["RateName"]),
                        });
                    }
                }
            }

            return dataList;
        }
    }
}
