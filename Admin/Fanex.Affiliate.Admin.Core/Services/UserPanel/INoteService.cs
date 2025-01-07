using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;

namespace Fanex.Affiliate.Admin.Core.Services.UserPanel
{
    public interface INoteService
    {
        EntityResult<NoteInfo> GetNoteList(AffiliatePagingParam param);

        NoteBasicInfo GetNoteContentById(int affId, int noteId);

        ResultInfo InsertNote(InsertUpdateNoteParam param);

        ResultInfo UpdateNote(InsertUpdateNoteParam param);
    }
}
