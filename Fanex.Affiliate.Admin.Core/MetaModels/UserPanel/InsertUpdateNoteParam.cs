namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class InsertUpdateNoteParam
    {
        public int AffId { get; set; }

        public int UserId { get; set; }

        public int NoteId { get; set; }

        public string NoteContent { get; set; }

        public string Ip { get; set; }
    }
}
