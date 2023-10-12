using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public class CommonMediaGalleryService : BaseRepository, ICommonMediaGalleryService
    {
        public IEnumerable<Category> GetCategoryList(byte typeId)
        {
            const string SpKey = "Aff_Admin_MediaCategory_Sel";
            var result = new List<Category>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("TypeId", typeId);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Category
                        {
                            CategoryName = Convert.ToString(reader["Category"])
                        });
                    }
                }
            }

            return result;
        }
    }
}
