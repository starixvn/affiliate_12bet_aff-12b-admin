
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery
{
    public class SelectOfferAffParam: SelectNewsLetterParam
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string AffName { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FileName { get; set; }

        public int UserId { get; set; }
    }
}
