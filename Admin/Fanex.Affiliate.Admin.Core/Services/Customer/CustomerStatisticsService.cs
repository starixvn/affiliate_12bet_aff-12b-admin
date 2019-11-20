using System;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO.Customer;
using Fanex.Affiliate.Admin.Core.MetaModels.Customer;
using Fanex.Affiliate.Admin.Core.Repository;

namespace Fanex.Affiliate.Admin.Core.Services.Customer
{
    public class CustomerStatisticsService : BaseRepository, ICustomerStatisticsService
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
                    if (reader.Read())
                    {
                        result = new CustomerStatistics
                        {
                            NewSignUpByMonth = Convert.ToInt32(reader["SignupsByMonth"]),
                            NewFdmByMonth = Convert.ToInt32(reader["FDMByMonth"]),
                            NewSignUpTotal = Convert.ToInt32(reader["SignupsTotal"]),
                            NewFdmTotal = Convert.ToInt32(reader["FDMTotal"]),
                            ActivePlayerByMonth = Convert.ToInt32(reader["ActivePlayersByMonth"]),
                            QualifiedMemberByMonth = Convert.ToInt32(reader["QualifiedMembersByMonth"]),
                            DepositByMonth = Convert.ToDecimal(reader["DepositByMonth"]),
                            DepositTotal = Convert.ToDecimal(reader["DepositTotal"]),
                            TurnoverByMonth = Convert.ToDecimal(reader["TurnoverByMonth"]),
                            TurnoverTotal = Convert.ToDecimal(reader["TurnoverTotal"]),
                            WinlostByMonth = Convert.ToDecimal(reader["WinlostByMonth"]),
                            WinlostTotal = Convert.ToDecimal(reader["WinlostTotal"]),
                            TotalClickByMonth = Convert.ToInt32(reader["TotalClickByMonth"]),
                            UniqueClickByMonth = Convert.ToInt32(reader["UniqueClickByMonth"]),
                            UniqueClickPcLaptopByMonth = Convert.ToInt32(reader["UniqueClickPCByMonth"]),
                            TotalClickPcLaptopByMonth = Convert.ToInt32(reader["TotalClickPCByMonth"]),
                            UniqueClickMobileByMonth = Convert.ToInt32(reader["UniqueClickMobileByMonth"]),
                            TotalClickMobileByMonth = Convert.ToInt32(reader["TotalClickMobileByMonth"])
                        };
                    }
                }
            }

            return result;
        }
    }
}