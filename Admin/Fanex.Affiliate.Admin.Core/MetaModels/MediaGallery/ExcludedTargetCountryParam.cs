using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery
{
    public class ExcludedTargetCountryParam
    {
        public int UserId { get; set; }

        [SpParam(Ignored = true)]
        public int PageId { get; set; }
    }
}
