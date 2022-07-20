using System;

namespace Fanex.Affiliate.User.Core.Repository
{
    public class IpAddressRepository : BaseRepository, IIpAddressRepository
    {
        private readonly IObjectDBBuilder _builder;

        public IpAddressRepository(IObjectDBBuilder builder)
        {
            _builder = builder;
        }

        public string GetContryShortByIpAddress(string ip)
        {
            const string uSpKey = "Acc_GetIPInfo_Full";

            using (var db = _builder.Build(uSpKey))
            {
                db.SetParameterValue("ip", ip);

                using (var reader = db.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var countryShort = Convert.ToString(reader["countrySHORT"]);

                        if(string.IsNullOrEmpty(countryShort) && reader.NextResult())
                        {
                            countryShort = Convert.ToString(reader["countrySHORT"]);
                        }

                        return countryShort;
                    }
                }
            }

            return string.Empty;
        }
    }
}
