using System;
using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public class StatementHistoryRepository : IStatementHistoryRepository
    {
        private readonly IObjectDBBuilder _builder;

        public StatementHistoryRepository(IObjectDBBuilder builder)
        {
            _builder = builder;
        }

        public IEnumerable<StatementHistory> GetStatementHistory(StatementHistoryParam statementHistoryParam)
        {
            const string uSpKey = "Aff_User_StatementHistory_Sel";
            var dbMode = statementHistoryParam.IsInternal ? DBMode.View : DBMode.Main;
            var dataList = new List<StatementHistory>();

            using (var db = _builder.Build(uSpKey, dbMode))
            {
                using (var reader = db.ExecuteReader(statementHistoryParam))
                {
                    while (reader.Read())
                    {
                        var transType = Convert.ToByte(reader["TransType"]);
                        var monthDate = Convert.ToDateTime(reader["MonthDate"]);

                        dataList.Add(new StatementHistory
                        {
                            MonthDate = monthDate,
                            TransType = transType,
                            Amount = reader["Amount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Amount"]),
                            Description = Convert.ToString(reader["Description"]),
                            Status = Convert.ToString(reader["Status"]),
                        });
                    }
                }
            }

            return dataList;
        }

        public IEnumerable<ProductItem> GetProductList(bool isInternal)
        {
            const string uSpKey = "Aff_ProductList_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpKey, dbMode))
            {
                return db.Query<ProductItem>();
            }
        }

        public DailyEarningResult GetDailyEarning(DailyEarningParam dailyEarningParam)
        {
            const string uSpKey = "Aff_DailyEarning_Sel";
            var dbMode = dailyEarningParam.IsInternal ? DBMode.View : DBMode.Main;

            var dailyEarningResult = new DailyEarningResult();
            var dataList = new List<DailyEarning>();

            using (var db = _builder.Build(uSpKey, dbMode))
            {
                using (var reader = db.ExecuteReader(dailyEarningParam))
                {
                    while (reader.Read())
                    {
                        dataList.Add(new DailyEarning
                        {
                            WinlostDate = Convert.ToDateTime(reader["WinlostDate"]),
                            Deposit = Convert.ToDecimal(reader["Deposit"]),
                            Withdrawal = Convert.ToDecimal(reader["Withdrawal"]),
                            MemberWinLost = Convert.ToDecimal(reader["MemberWinLost"]),
                            MemberWinLostFake = Convert.ToDecimal(reader["MemberWinLostFake"]),
                            Bonus = Convert.ToDecimal(reader["Bonus"]),
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
                            MemberWinLostFake = Convert.ToDecimal(reader["MemberWinLostFake"]),
                            Bonus = Convert.ToDecimal(reader["Bonus"]),
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
            var result = new ExtraUserInfo();

            using (var db = _builder.Build(uSpKey, mode))
            {
                db.AddInParameter("AffId", System.Data.SqlDbType.Int, affId);

                using (var reader = db.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result.GroupName = Convert.ToString(reader["GroupName"]);
                        result.HasDailyDetail = Convert.ToBoolean(reader["HasDailyDetail"]);
                    }
                }
            }

            return result;
        }

        public IEnumerable<DailyEarningDetail> GetDailyEarningDetail(DailyEarningParam earningDetailParam)
        {
            const string uSpKey = "Aff_DailyEarning_Detail_Sel";
            var mode = earningDetailParam.IsInternal ? DBMode.View : DBMode.Main;
            var result = new List<DailyEarningDetail>();

            using (var db = _builder.Build(uSpKey, mode))
            {
                using (var reader = db.ExecuteReader(earningDetailParam))
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
                            WinLost = Convert.ToDecimal(reader["Winlost"])
                        });
                    }
                }
            }

            return result;
        }
    }
}