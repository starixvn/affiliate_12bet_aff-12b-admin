using System;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO.Customer;
using Fanex.Affiliate.Admin.Core.MetaModels.Customer;

namespace Fanex.Affiliate.Admin.Core.Repository.Customer
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public CustomerStatistics GetCustomerStatistics(CustomerStatisticsParam param)
        {
            const string SpKey = "Aff_Admin_Statistics_Sel";
            CustomerStatistics result = null;

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("AffID", DbType.Int32, param.AffId);
                objectDb.AddInParameter("MonthDate", DbType.DateTime, param.MonthDate.Value);

                using (var reader = objectDb.ExecuteReader())
                {
                    result = new CustomerStatistics
                    {
                        NewSignUp = Convert.ToInt32(reader["NewSignUp"]),
                        NewFdm = Convert.ToInt32(reader["NewSignUpDep"]),
                        TotalNewSignUp = Convert.ToInt32(reader["TotalPlayer"]),
                        TotalNewFdm = Convert.ToInt32(reader["TotalPlayerDepUp"]),
                        TotalActivePlayer = Convert.ToInt32(reader["TotalActivePlayer"]),
                        TotalQualifiedActivePlayer = Convert.ToInt32(reader["TotalQualifiedMembers"]),
                        TotalDeposit = Convert.ToDecimal(reader["TotalDeposit"]),
                        TotalDepositUp = Convert.ToDecimal(reader["TotalDepositUp"]),
                        Turnover = Convert.ToDecimal(reader["TotalTurnover"]),
                        TotalTurnover = Convert.ToDecimal(reader["TotalTurnoverUp"]),
                        Winlost = Convert.ToDecimal(reader["TotalWinlost"]),
                        TotalWinlost = Convert.ToDecimal(reader["TotalWinlostUp"]),
                        TotalClick = Convert.ToInt32(reader["TotalClicks"]),
                        UniqueClick = Convert.ToInt32(reader["TotalUniqueClicks"]),
                        UniqueClickByPcLaptop = Convert.ToInt32(reader["TotalUniqueClicksBySite"]),
                        TotalClickByPcLaptop = Convert.ToInt32(reader["TotalClicksBySite"]),
                        UniqueClickByMobile = Convert.ToInt32(reader["TotalUniqueClicksByMobile"]),
                        TotalClickByMobile = Convert.ToInt32(reader["TotalClicksByMobile"])
                    };
                }
            }

            return result;
        }
    }
}