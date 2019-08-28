using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.ExchangeRate;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Repository.ExchangeRate
{
    public class ExchangeRateRepository : BaseRepository, IExchangeRateRepository
    {
        public ExchangeRateInfo GetExchangeRateDetail()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExchangeRateInfo> GetExchangeRateList(DateTime date)
        {
            string SpKey = "Aff_ExchangeSel";
            var result = new List<ExchangeRateInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("time", DbType.DateTime, date);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new ExchangeRateInfo
                        {
                            ExchangeId = Convert.ToInt32(reader["ExchangeId"]),
                            Currency = Convert.ToString(reader["Currency"]),
                            ActualRate = Convert.ToDouble(reader["ActualRate"]),
                            BuyingRate = Convert.ToDouble(reader["BuyingRate"])
                        });
                    }
                }
            }

            return result;
        }

        public ResultInfo UpdateExchangeRate()
        {
            throw new NotImplementedException();
        }
    }
}
