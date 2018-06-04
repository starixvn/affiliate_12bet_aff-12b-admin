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
    }
}