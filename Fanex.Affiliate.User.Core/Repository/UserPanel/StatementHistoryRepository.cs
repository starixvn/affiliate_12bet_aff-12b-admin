using System;
using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public class StatementHistoryRepository : IStatementHistoryRepository
    {
        public IEnumerable<StatementHistory> GetStatementHistory(StatementHistoryParam statementHistoryParam)
        {
            const string uSpKey = "Aff_AffiliateStatementHistorySel";
            var dbMode = statementHistoryParam.IsInternal ? DBMode.View : DBMode.Main;
            var dataList = new List<StatementHistory>();

            using (var objectDb = new ObjectDBBuilder(uSpKey, dbMode))
            {
                using (var reader = objectDb.DB.ExecuteReader(statementHistoryParam))
                {
                    while (reader.Read())
                    {
                        var transType = Convert.ToByte(reader["TransType"]);
                        var monthDate = Convert.ToDateTime(reader["MonthDate"]);

                        dataList.Add(new StatementHistory
                        {
                            TransId = Convert.ToInt64(reader["TransId"]),
                            MonthDate = monthDate,
                            TransType = transType,
                            Amount = reader["Amount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Amount"]),
                            Creator = Convert.ToString(reader["Creator"]),
                            Description = transType == (byte)Enum.TransType.Payment ? Convert.ToString(reader["Description"]) : string.Empty,
                            Status = Convert.ToString(reader["Status"]),
                            IsLink = transType != (byte)Enum.TransType.Payment &&
                                        transType != (byte)Enum.TransType.Adjustment &&
                                        DateTime.Compare(monthDate, statementHistoryParam.AvailableFromDate) > 0
                        });
                    }
                }
            }

            return dataList;
        }

        public IEnumerable<SportTypeItem> GetSportTypeList(int affId, bool isInternal)
        {
            const string uSpKey = "Aff_SporttypeSelByAff";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var objectDb = new ObjectDBBuilder(uSpKey, dbMode))
            {
                return objectDb.DB.Query<SportTypeItem>(new { affId });
            }
        }

        public DailyEarningResult GetDailyEarning(DailyEarningParam dailyEarningParam)
        {
            const string uSpKey = "Aff_EarningSelDaily";
            var dbMode = dailyEarningParam.IsInternal ? DBMode.View : DBMode.Main;

            var dailyEarningResult = new DailyEarningResult();
            var dataList = new List<DailyEarning>();

            using (var objectDb = new ObjectDBBuilder(uSpKey, dbMode))
            {
                using (var reader = objectDb.DB.ExecuteReader(dailyEarningParam))
                {
                    while (reader.Read())
                    {
                        dataList.Add(new DailyEarning
                        {
                            WinlostDate = Convert.ToDateTime(reader["WinlostDate"]),
                            Deposit = Convert.ToDecimal(reader["Deposit"]),
                            Withdrawal = Convert.ToDecimal(reader["Withdrawal"]),
                            MemberWinLost = Convert.ToDecimal(reader["MemberWinLost"]),
                            MemberWinLostFake = Convert.ToDecimal(reader["MemWinLostFake"]),
                            Bonus = Convert.ToDecimal(reader["Bonus"]),
                            Adjustment = Convert.ToDecimal(reader["Adjustment"]),
                            Psp = Convert.ToDecimal(reader["Psp"])
                        });
                    }
                    dailyEarningResult.MainDataList = dataList;

                    if (reader.NextResult() && reader.Read())
                    {
                        dailyEarningResult.Total = new DailyEarning
                        {
                            Deposit = Convert.ToDecimal(reader["Deposit"]),
                            Withdrawal = Convert.ToDecimal(reader["Withdrawal"]),
                            MemberWinLost = Convert.ToDecimal(reader["MemberWinLost"]),
                            MemberWinLostFake = Convert.ToDecimal(reader["MemWinLostFake"]),
                            Bonus = Convert.ToDecimal(reader["Bonus"]),
                            Adjustment = Convert.ToDecimal(reader["Adjustment"]),
                            Psp = Convert.ToDecimal(reader["Psp"])
                        };
                    }
                }
            }

            return dailyEarningResult;
        }

        public ExtraUserInfo GetExtraUserInfo(int affId, bool isInternal)
        {
            const string uSpKey = "Aff_AffiliateUser_ExtraUserInfoSel";
            var mode = isInternal ? DBMode.View : DBMode.Main;

            using (var objectDb = new ObjectDBBuilder(uSpKey, mode))
            {
                return objectDb.DB.QueryEntity<ExtraUserInfo>(new { affId });
            }
        }

        public IEnumerable<DailyEarningDetail> GetDailyEarningDetail(DailyEarningParam earningDetailParam)
        {
            const string uSpKey = "Aff_EarningSelDetail";
            var mode = earningDetailParam.IsInternal ? DBMode.View : DBMode.Main;

            using (var objectDb = new ObjectDBBuilder(uSpKey, mode))
            {
                return objectDb.DB.Query<DailyEarningDetail>(earningDetailParam);
            }
        }
    }
}