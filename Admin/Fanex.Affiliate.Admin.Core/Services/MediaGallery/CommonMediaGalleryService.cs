using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.Repository;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public class CommonMediaGalleryService : BaseRepository, ICommonMediaGalleryService
    {
        IEnumerable<MediaGalleryFilterItem> ICommonMediaGalleryService.GetMediaGalleryFilterItemList(string typeName)
        {
            const string SpKey = "Aff_BannerSelType_1";
            var result = new List<MediaGalleryFilterItem>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("TypeName", DbType.String, typeName);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new MediaGalleryFilterItem
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = Convert.ToString(reader["Value"])
                        });
                    }
                }
            }

            return result;
        }
    }
}
