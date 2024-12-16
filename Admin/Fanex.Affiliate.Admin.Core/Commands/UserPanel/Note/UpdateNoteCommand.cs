using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.Commands.UserPanel.Note
{
    public class UpdateNoteCommand : BaseAffNonQueryCommand
    {
        private const string SP_NAME = "Aff_Admin_AffiliateNote_Upd";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0;

        public int NoteId { get; set; }

        [SpParam(SqlParamName = "Note")]
        public string NoteContent { get; set; }
    }
}
