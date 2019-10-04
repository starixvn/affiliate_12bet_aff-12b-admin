using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.ExchangeRate;
using System.Data;
using Fanex.Affiliate.Admin.Core.MetaModels.ExchangeRate;
using Fanex.Affiliate.Admin.Core.Repository;

namespace Fanex.Affiliate.Admin.Core.Services.ExchangeRate
{
    public class ExchangeRateService : BaseRepository, IExchangeRateService
    {
        public IEnumerable<ExchangeRateInfo> GetExchangeRateList(DateTime date)
        {
            const string SpKey = "Aff_ExchangeSel";
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
                            ActualRate = Convert.ToDecimal(reader["ActualRate"]),
                            BuyingRate = Convert.ToDecimal(reader["BuyingRate"])
                        });
                    }
                }
            }

            return result;
        }

        public ResultInfo UpdateExchangeRate(ExchangeRateParam param)
        {
            const string SpKey = "Aff_ExchangeUpdate";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("CustId", DbType.Int32, param.UserId);
                objectDb.AddInParameter("ExchangeId", DbType.Int32, param.ExchangeId);
                objectDb.AddInParameter("Rate", DbType.Decimal, param.Rate);
                objectDb.AddInParameter("IP", DbType.String, param.Ip);
                objectDb.AddInParameter("RateType", DbType.Byte, param.RateType);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }
    }
}
