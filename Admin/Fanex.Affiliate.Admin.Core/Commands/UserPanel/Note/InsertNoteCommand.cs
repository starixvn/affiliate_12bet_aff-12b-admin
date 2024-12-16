using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.Commands.UserPanel.Note
{
    public class InsertNoteCommand : BaseAffNonQueryCommand
    {
        private const string SP_NAME = "Aff_Admin_AffiliateNote_Ins";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0;

        [SpParam(SqlParamName = "Note")]
        public string NoteContent { get; set; }
    }
}
