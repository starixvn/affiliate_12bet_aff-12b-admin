using Fanex.Affiliate.Admin.Core.DTO.Common;
using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Repository.Common
{
    public class SystemRepository : BaseRepository, ISystemRepository
    {
        public IEnumerable<Product> GetProductList()
        {
            const string SpKey = "Aff_ProductList_Sel";

            using (var objectDb = GetObjectDb(SpKey))
            {
                return objectDb.Query<Product>();
            }
        }

        public IEnumerable<Country> GetCountryList()
        {
            const string SpKey = "Aff_Country_Sel";

            using (var objectDb = GetObjectDb(SpKey))
            {
                return objectDb.Query<Country>();
            }
        }

        public IEnumerable<Language> GetLanguageList()
        {
            const string SpKey = "Aff_Market_Sel";
            var result = new List<Language>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Language
                        {
                            LanguageCode = Convert.ToString(reader["MarketId"]),
                            LanguageName = Convert.ToString(reader["MarketName"]),
                            ShortedCode = Convert.ToString(reader["ShortedCode"])
                        });
                    }
                }
            }

            return result;
        }

        public IEnumerable<AffType> GetAffTypeList()
        {
            const string SpKey = "Aff_AffTypeSel";

            var result = new List<AffType>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new AffType
                        {
                            AffTypeId = Convert.ToInt32(reader["TypeId"]),
                            AffTypeName = Convert.ToString(reader["TypeName"])
                        });
                    }
                }
            }

            return result;
        }

        public IEnumerable<TargetCountry> GetTargetCountryList(int userId)
        {
            const string SpKey = "Aff_TargetCountry_Sel";

            using (var objectDb = GetObjectDb(SpKey))
            {
                return objectDb.Query<TargetCountry>(new { userId });
            }
        }

        public IEnumerable<Language> GetTargetLanguageList(int userId)
        {
            const string SpKey = "Aff_Language_Sel";

            using (var objectDb = GetObjectDb(SpKey))
            {
                return objectDb.Query<Language>(new { userId });
            }
        }
    }
}
