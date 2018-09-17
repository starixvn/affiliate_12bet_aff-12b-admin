using System;
using System.Collections.Generic;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO.Report;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Report;
using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public class AffiliateReportRepository : BaseRepository, IAffiliateReportRepository
    {
        public AffiliateReportMainEntityResult GetMainReportData(AffiliateReportMainParam param)
        {
            const string SpKey = "Aff_Admin_AffiliateReport_Sel";
            var result = new AffiliateReportMainEntityResult();
            var dataList = new List<AffiliateReportMain>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignMainReportParam(objectDb, param);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(BuildReportMainItem(reader));
                    }

                    if (reader.NextResult() && reader.Read())
                    {
                        result.Total = BuildReportMainItem(reader);
                    }
                }

                result.MainData = dataList;
                var totalRow = objectDb.GetParameterValue("NRows");
                result.TotalItem = totalRow == DBNull.Value ? 0 : Convert.ToInt32(totalRow);
            }

            return result;
        }

        private static void AssignMainReportParam(IObjectDb objectDb, AffiliateReportMainParam param)
        {
            objectDb.AddInParameter("Username", System.Data.DbType.String, param.UserName);
            objectDb.AddInParameter("FromDate", System.Data.DbType.DateTime, param.FromDate);
            objectDb.AddInParameter("ToDate", System.Data.DbType.DateTime, param.ToDate);
            objectDb.AddInParameter("PreferredLanguage", System.Data.DbType.String, param.PreferredLanguage);
            objectDb.AddInParameter("ProductIds", System.Data.DbType.String, param.ProductIds);
            objectDb.AddInParameter("SortType", System.Data.DbType.Byte, param.SortType);
            objectDb.AddInParameter("SortDirectionType", System.Data.DbType.Byte, param.SortDirectionType);
            objectDb.AddInParameter("PageIndex", System.Data.DbType.Int32, param.PageIndex);
            objectDb.AddInParameter("PageSize", System.Data.DbType.Int32, param.PageSize);

            objectDb.AddOutParameter("NRows", System.Data.DbType.Int32, 4);
        }

        private static AffiliateReportMain BuildReportMainItem(IDataReader reader)
        {
            return new AffiliateReportMain
            {
                AffId = reader.HasColumn("AffId") ? Convert.ToInt32(reader["AffId"]) : 0,
                UserName = reader.HasColumn("Username") ? Convert.ToString(reader["Username"]) : string.Empty,
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
                CommissionRate = decimal.Round(100 * Convert.ToDecimal(reader["CommissionRate"]), 2),
                Earning = Convert.ToDecimal(reader["Earning"]),
                AffWithdrawal = Convert.ToDecimal(reader["AffWithdrawal"]),
                AffAdjustment = Convert.ToDecimal(reader["AffAdjustment"]),
                BalanceBf = Convert.ToDecimal(reader["BFF"])
            };
        }
    }
}