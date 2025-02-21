namespace Fanex.Affiliate.Admin.Core.Commands.UserPanel.Note
{
    public class UpdateNoteCommand : InsertNoteCommand
    {
        public UpdateNoteCommand(string spPrefix, int noteId) : base(spPrefix)
        {
            NoteId = noteId;
        }

        public override string GetSettingKey() => $"{_spPrefix}_Upd";

        public int NoteId { get; }
    }
}
