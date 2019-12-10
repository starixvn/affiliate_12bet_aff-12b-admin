using Newtonsoft.Json;
using System;

namespace Fanex.Affiliate.Admin.Core.DTO.MediaGallery
{
    [Serializable]
    public class Category
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        [JsonIgnore]
        public int ParentCategoryId { get; set; }
    }
}
