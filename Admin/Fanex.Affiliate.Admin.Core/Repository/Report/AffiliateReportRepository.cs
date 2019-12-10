using System;
using System.Collections.Generic;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Report;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Report;
using Fanex.Data;
using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.Repository.Report
{
    public class AffiliateReportRepository : BaseRepository, IAffiliateReportRepository
    {
        private const int OneHundred = 100;
        private const int NumberOfDigits = 2;

        public EntityResult<AffiliateReport> GetMainReportData(AffiliateReportMainParam param)
        {
            const string SpKey = "Aff_Admin_AffiliateReport_Sel";
            var result = new EntityResult<AffiliateReport>();
            var dataList = new List<AffiliateReport>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignCommonParam(objectDb, param);
                objectDb.AddInParameter("PreferredLanguage", DbType.String, param.PreferredLanguage);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(BuildDataItem(reader));
                    }

                    if (reader.NextResult() && reader.Read())
                    {
                        result.Total = BuildDataItem(reader);
                    }
                }

                result.MainData = dataList;
                var totalRow = objectDb.GetParameterValue("NRows");
                result.TotalItem = totalRow == DBNull.Value ? 0 : Convert.ToInt32(totalRow);
            }

            return result;
        }

        public EntityResult<AffiliateReport> GetDetailReportData(AffiliateReportMainParam param)
        {
            const string SpKey = "Aff_Admin_AffiliateReport_Detail_Sel";
            var result = new EntityResult<AffiliateReport>();

            var dataList = new List<AffiliateReport>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignCommonParam(objectDb, param);
                objectDb.AddInParameter("IsGetAll", DbType.Boolean, param.IsGetAll);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(BuildDataItem(reader));
                    }

                    if (reader.NextResult() && reader.Read())
                    {
                        result.Total = BuildDataItem(reader);
                    }
                }

                result.MainData = dataList;
                var totalRow = objectDb.GetParameterValue("NRows");
                result.TotalItem = totalRow == DBNull.Value ? 0 : Convert.ToInt32(totalRow);
            }

            return result;
        }

        public IList<string> SearchAffiliateList(string userName)
        {
            const string SpKey = "Aff_AffiliateUsername_SearchEx";
            var result = new List<string>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("UserName", DbType.String, userName);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(Convert.ToString(reader["UserName"]));
                    }
                }
            }

            return result;
        }

        private static void AssignCommonParam(IObjectDb objectDb, AffiliateReportMainParam param)
        {
            objectDb.AddInParameter("Username", DbType.String, param.UserName);
            objectDb.AddInParameter("FromDate", DbType.DateTime, param.FromDate);
            objectDb.AddInParameter("ToDate", DbType.DateTime, param.ToDate);
            objectDb.AddInParameter("ProductIds", DbType.String, param.ProductIds);
            objectDb.AddInParameter("SortType", DbType.Byte, param.SortType);
            objectDb.AddInParameter("SortDirectionType", DbType.Byte, param.SortDirectionType);
            objectDb.AddInParameter("PageIndex", DbType.Int32, param.PageIndex);
            objectDb.AddInParameter("PageSize", DbType.Int32, param.PageSize);
            objectDb.AddInParameter("IsExportExcel", DbType.Boolean, param.IsExportExcel);

            objectDb.AddOutParameter("NRows", DbType.Int32, Size.SizeOfInt);
        }

        private static AffiliateReport BuildDataItem(IDataReader reader)
        {
            return new AffiliateReport
            {
                AffId = reader.HasColumn("AffId") ? Convert.ToInt32(reader["AffId"]) : 0,
                UserName = reader.HasColumn("Username") ? Convert.ToString(reader["Username"]) : string.Empty,
                MonthDate = reader.HasColumn("MonthDate") ? Convert.ToDateTime(reader["MonthDate"]) : (DateTime?)null,
                Description = reader.HasColumn("Description") ? Convert.ToString(reader["Description"]) : string.Empty,
                TotalClick = Convert.ToInt32(reader["TotalClick"]),
                UniqueClick = Convert.ToInt32(reader["UniqueClick"]),
                Signup = Convert.ToInt32(reader["Signup"]),
                Fdm = Convert.ToInt32(reader["FDM"]),
                ActiveMember = Convert.ToInt32(reader["ActiveMember"]),
                QualifiedMember = Convert.ToInt32(reader["QualifiedMember"]),
                Deposit = Convert.ToDecimal(reader["Deposit"]),
                Withdrawal = Convert.ToDecimal(reader["Withdrawal"]),
                Turnover = Convert.ToDecimal(reader["Turnover"]),
                Winlost = Convert.ToDecimal(reader["Winlost"]),
                Bonus = Convert.ToDecimal(reader["Bonus"]),
                PlatformFee = Convert.ToDecimal(reader["PlatformFee"]),
                OperationalFee = Convert.ToDecimal(reader["OperationalFee"]),
                BankingFee = Convert.ToDecimal(reader["BankingFee"]),
                TotalFee = Convert.ToDecimal(reader["TotalFee"]),
                CommissionRate = decimal.Round(OneHundred * Convert.ToDecimal(reader["CommissionRate"]), NumberOfDigits),
                Earning = Convert.ToDecimal(reader["Earning"]),
                AffWithdrawal = Convert.ToDecimal(reader["AffWithdrawal"]),
                AffAdjustment = Convert.ToDecimal(reader["AffAdjustment"]),
                BalanceBf = Convert.ToDecimal(reader["BFF"])
            };
        }
    }
}