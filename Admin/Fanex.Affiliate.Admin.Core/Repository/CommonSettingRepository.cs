using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.Common;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public class CommonSettingRepository : BaseRepository, ICommonSettingRepository
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
    }
}
