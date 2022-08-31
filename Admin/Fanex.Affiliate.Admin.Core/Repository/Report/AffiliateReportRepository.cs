using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Report;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Report;
using Fanex.Data;
using System;
using System.Collections.Generic;
using System.Data;

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
                objectDb.SetParameterValue("IsGetMonthly", param.IsGetMonthly);
                objectDb.SetParameterValue("PreferredLanguages", param.PreferredLanguages);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(BuildDataItem(reader));
                    }

                    if (reader.NextResult() && reader.Read())
                    {
                        result.Total = BuildDataItem(reader, true);
                    }
                }

                result.MainData = dataList;
                result.TotalItem = GetTotalRow(objectDb);
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
                objectDb.SetParameterValue("IsGetAll", param.IsGetAll);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(BuildDataItem(reader, isDetail: true));
                    }

                    if (reader.NextResult() && reader.Read())
                    {
                        result.Total = BuildDataItem(reader, true, true);
                    }
                }

                result.MainData = dataList;
                result.TotalItem = GetTotalRow(objectDb);
            }

            return result;
        }

        public IList<string> SearchAffiliateList(string userName)
        {
            const string SpKey = "Aff_AffiliateUsername_SearchEx";
            var result = new List<string>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("UserName", userName);

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
            objectDb.SetParameterValue("Username", param.UserName);
            objectDb.SetParameterValue("FromDate", param.FromDate);
            objectDb.SetParameterValue("ToDate", param.ToDate);
            objectDb.SetParameterValue("ProductIds", param.ProductIds);
            objectDb.SetParameterValue("SortType", param.SortType);
            objectDb.SetParameterValue("SortDirectionType", param.SortDirectionType);
            objectDb.SetParameterValue("PageIndex", param.PageIndex);
            objectDb.SetParameterValue("PageSize", param.PageSize);
            objectDb.SetParameterValue("IsExportExcel", param.IsExportExcel);

            objectDb.SetParameterValue("NRows", DefaultValue.Int);
        }

        private static AffiliateReport BuildDataItem(IDataReader reader, bool isTotal = false, bool isDetail = false)
        {
            var affReport = new AffiliateReport
            {
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

            if (isDetail)
            {
                affReport.PreviousBalanceBf = reader.HasColumn("PrevBFF") ? Convert.ToDecimal(reader["PrevBFF"]) : DefaultValue.Decimal;

                if(!isTotal)
                {
                    affReport.MonthDate = Convert.ToDateTime(reader["MonthDate"]);
                    affReport.Description = Convert.ToString(reader["Description"]);
                }    
            }
            else
            {
                if (!isTotal)
                {
                    affReport.AffId = Convert.ToInt32(reader["AffId"]);
                    affReport.UserName = Convert.ToString(reader["Username"]);
                    affReport.PreferredLanguage = Convert.ToString(reader["Language"]);
                }
            }

            return affReport;
        }
    }
}