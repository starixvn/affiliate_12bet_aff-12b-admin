using Fanex.Affiliate.Admin.Core.Commands.UserPanel.Note;
using Fanex.Affiliate.Admin.Core.Criterias.UserPanel;
using Fanex.Affiliate.Admin.Core.Criterias.UserPanel.Note;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Services.UserPanel
{
    public class NoteService : BaseDynamicService, INoteService
    {
        public NoteService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

        public EntityResult<NoteDetail> GetNoteList(AffiliatePagingParam param)
        {
            var criteria = new NoteCriteria();

            return FetchEntityResult<NoteDetail>(criteria, param);
        }

        public Note GetNoteContentById(int affId, int noteId)
        {
            var criteria = new NoteDetailCriteria
            {
                AffId = affId,
                NoteId = noteId,
            };

            return GetTData<Note>(criteria);
        }

        public ResultInfo InsertNote(InsertUpdateNoteParam param)
        {
            var command = new InsertNoteCommand
            {
                NoteContent = param.NoteContent,
            };

            return ExecuteReturnResultInfo(command, param);
        }

        public ResultInfo UpdateNote(InsertUpdateNoteParam param)
        {
            var command = new UpdateNoteCommand
            {
                NoteId = param.NoteId,
                NoteContent = param.NoteContent,
            };

            return ExecuteReturnResultInfo(command, param);
        }
    }
}
