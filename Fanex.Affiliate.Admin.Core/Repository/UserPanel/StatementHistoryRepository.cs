using System;
using System.Collections.Generic;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;

namespace Fanex.Affiliate.Admin.Core.Repository.UserPanel
{
    public class StatementHistoryRepository : BaseRepository, IStatementHistoryRepository
    {
        public DailyEarningResult GetDailyEarning(DailyEarningParam param)
        {
            const string uSpKey = "Aff_DailyEarning_Sel";

            var dailyEarningResult = new DailyEarningResult();
            var dataList = new List<DailyEarning>();

            using (var objectDb = GetObjectDb(uSpKey))
            {
                objectDb.AddInParameter("AffId", DbType.Int32, param.AffId);
                objectDb.AddInParameter("ProductId", DbType.Int16, param.ProductId);
                objectDb.AddInParameter("MonthDate", DbType.DateTime, param.MonthDate);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(BuildDailyEarningItem(reader));
                    }
                    dailyEarningResult.MainData = dataList;

                    if (reader.NextResult() && reader.Read())
                    {
                        dailyEarningResult.Total = BuildDailyEarningItem(reader);
                    }
                }
            }

            return dailyEarningResult;
        }

        public IEnumerable<DailyEarningDetail> GetDailyEarningDetail(DailyEarningParam param)
        {
            const string SpKey = "Aff_DailyEarning_Detail_Sel";
            var result = new List<DailyEarningDetail>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("AffId", DbType.Int32, param.AffId);
                objectDb.AddInParameter("ProductId", DbType.Int32, param.ProductId);
                objectDb.AddInParameter("MonthDate", DbType.Int32, param.MonthDate);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new DailyEarningDetail
                        {
                            CustId = Convert.ToInt32(reader["CustId"]),
                            UserName = Convert.ToString(reader["UserName"]),
                            Deposit = Convert.ToDecimal(reader["Deposit"]),
                            Withdrawal = Convert.ToDecimal(reader["Withdrawal"]),
                            Turnover = Convert.ToDecimal(reader["Turnover"]),
                            WinLost = Convert.ToDecimal(reader["Winlost"]),
                            Bonus = Convert.ToDecimal(reader["Bonus"]),
                            BankingFee = Convert.ToDecimal(reader["BankingFee"])
                        });
                    }
                }
            }

            return result;
        }

        private static DailyEarning BuildDailyEarningItem(IDataReader reader)
        {
            return new DailyEarning
            {
                WinlostDate = reader.HasColumn("WinlostDate") ? Convert.ToDateTime(reader["WinlostDate"]) : (DateTime?)null,
                Deposit = Convert.ToDecimal(reader["Deposit"]),
                Withdrawal = Convert.ToDecimal(reader["Withdrawal"]),
                MemberWinLost = Convert.ToDecimal(reader["MemberWinLost"]),
                Bonus = Convert.ToDecimal(reader["Bonus"]),
                BankingFee = reader["BankingFee"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["BankingFee"])
            };
        }
    }
}
