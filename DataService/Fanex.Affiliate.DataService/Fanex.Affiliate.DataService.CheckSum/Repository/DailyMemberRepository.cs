using System;
using System.Collections.Generic;
using System.Data;
using Fanex.Affiliate.DataService.CheckSum.DTO;
using Fanex.Affiliate.DataService.CheckSum.ViewModel;
using Fanex.Data;

namespace Fanex.Affiliate.DataService.CheckSum.Repository
{
    public class DailyMemberRepository : BaseRepository, IDailyMemberRepository
    {
        public DailyMemberViewModel CheckSumDailyMemberFor12Bet(DateTime date)
        {
            const string SpName = "Aff_CheckSum";

            using (var objectDb = this.GetObjectDb(SpName))
            {
                AssignParam(objectDb, date);

                using (var reader = objectDb.ExecuteReader())
                {
                    return new DailyMemberViewModel
                    {
                        DateString = date.ToLongDateString(),
                        Data = GetDailyMemberDataFor12Bet(reader),
                        isMatch = Convert.ToBoolean(objectDb.GetParameterValue("ismatch"))
                    };
                }
            }
        }

        public DailyMemberIncomeAccessViewModel CheckSumDailyMemberForIncomeAccess(DateTime date)
        {
            const string SpName = "IA_CheckSum";

            using (var objectDb = this.GetObjectDb(SpName))
            {
                AssignParam(objectDb, date);

                using (var reader = objectDb.ExecuteReader())
                {
                    return new DailyMemberIncomeAccessViewModel
                    {
                        Data = GetDailyMemberDataForIncomeAccess(reader),
                        isMatch = Convert.ToBoolean(objectDb.GetParameterValue("ismatch")),
                        DateString = date.ToString()
                    };
                }
            }
        }

        private static void AssignParam(IObjectDb objectDb, DateTime date)
        {
            objectDb.SetParameterValue("date", date.Date);
            objectDb.SetParameterValue("ismatch", 4);
        }

        private static IEnumerable<DailyMember> GetDailyMemberDataFor12Bet(IDataReader reader)
        {
            var data = new List<DailyMember>();

            while (reader.Read())
            {
                data.Add(new DailyMember
                {
                    Deposit = Convert.ToDecimal(reader["Deposit"]),
                    Withdrawal = Convert.ToDecimal(reader["Withdrawal"]),
                    Bonus = Convert.ToDecimal(reader["Bonus"]),
                    Adjustment = Convert.ToDecimal(reader["Adjustment"]),
                    Turnover = Convert.ToDecimal(reader["Turnover"]),
                    Winlost = Convert.ToDecimal(reader["Winlost"]),
                    Description = reader["Description"].ToString()
                });
            }

            return data;
        }

        private static IEnumerable<DailyMemberIncomeAccess> GetDailyMemberDataForIncomeAccess(IDataReader reader)
        {
            var data = new List<DailyMemberIncomeAccess>();

            while (reader.Read())
            {
                data.Add(new DailyMemberIncomeAccess
                {
                    Deposit = Convert.ToDecimal(reader["Deposit"]),
                    Withdrawal = Convert.ToDecimal(reader["Withdrawal"]),
                    Bonus = Convert.ToDecimal(reader["Bonus"]),
                    Adjustment = Convert.ToDecimal(reader["Adjustment"]),
                    Turnover = Convert.ToDecimal(reader["Turnover"]),
                    Outstanding = Convert.ToDecimal(reader["Outstanding"]),
                    Winlost = Convert.ToDecimal(reader["Winlost"]),
                    DrawGame = Convert.ToDecimal(reader["DrawGame"]),
                    Description = reader["Description"].ToString()
                });
            }

            return data;
        }
    }
}