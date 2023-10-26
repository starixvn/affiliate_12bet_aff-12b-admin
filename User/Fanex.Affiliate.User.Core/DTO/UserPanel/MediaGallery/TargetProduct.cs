
using Fanex.Data;

namespace Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery
{
    public class TargetProduct
    {
        [SpParam(SqlParamName = "Category")]
        public string ProductId { get; set; }

        [SpParam(SqlParamName = "SubCategory")]
        public string ProductName { get; set; }
    }
}
