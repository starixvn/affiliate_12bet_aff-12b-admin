using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class InsertUpdateNoteParam : AffiliateBasicInfoParam
    {
        [Range(Validation.MinId, Validation.MaxId)]
        public int NoteId { get; set; }

        [Required]
        public string NoteContent { get; set; }
    }
}
