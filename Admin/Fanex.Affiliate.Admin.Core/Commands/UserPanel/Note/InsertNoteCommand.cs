using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.Commands.UserPanel.Note
{
    public class InsertNoteCommand : BaseAffNonQueryCommand
    {
        protected readonly string _spPrefix;

        public InsertNoteCommand(string spPrefix)
        {
            _spPrefix = spPrefix;
        }

        public override string GetSettingKey() => $"{_spPrefix}_Ins";

        public override bool IsValid() => !string.IsNullOrWhiteSpace(_spPrefix) && AffId > 0;

        [SpParam(SqlParamName = "Note")]
        public string NoteContent { get; set; }
    }
}
