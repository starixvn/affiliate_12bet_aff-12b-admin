using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;

namespace Fanex.Affiliate.Admin.Core.Repository.UserPanel
{
    public interface INoteRepository
    {
        EntityResult<Note> GetNoteList(NoteParam param);

        ResultInfo InsertNote(InsertUpdateNoteParam param);

        ResultInfo UpdateNote(InsertUpdateNoteParam param);
    }
}
