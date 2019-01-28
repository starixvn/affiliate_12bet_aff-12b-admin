using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;

namespace Fanex.Affiliate.Admin.Core.Repository.UserPanel
{
    public interface INoteRepository
    {
        EntityResult<Note> GetNoteList(NoteParam param);

        Note GetNoteById(int affId, int noteId);

        ResultInfo InsertNote(InsertUpdateNoteParam param);

        ResultInfo UpdateNote(InsertUpdateNoteParam param);
    }
}
