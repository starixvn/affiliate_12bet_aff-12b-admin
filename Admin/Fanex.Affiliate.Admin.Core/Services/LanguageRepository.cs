using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public class LanguageRepository : BaseRepository, ILanguageRepository
    {
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
                            LanguageName = Convert.ToString(reader["MarketName"])
                        });
                    }
                }
            }

            return result;
        }
    }
}
