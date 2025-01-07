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

        public EntityResult<NoteInfo> GetNoteList(AffiliatePagingParam param)
        {
            var criteria = new NoteCriteria
            {
                AffId = param.AffId,
                PageIndex = param.PageIndex,
                PageSize = param.PageSize,
            };

            return FetchEntityResult<NoteInfo>(criteria);
        }

        public NoteBasicInfo GetNoteContentById(int affId, int noteId)
        {
            var criteria = new NoteDetailCriteria
            {
                AffId = affId,
                NoteId = noteId,
            };

            return GetTData<NoteBasicInfo>(criteria);
        }

        public ResultInfo InsertNote(InsertUpdateNoteParam param)
        {
            var command = new InsertNoteCommand
            {
                AffId = param.AffId,
                UserId = param.UserId,
                Ip = param.Ip,
                NoteContent = param.NoteContent,
            };

            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo UpdateNote(InsertUpdateNoteParam param)
        {
            var command = new UpdateNoteCommand
            {
                AffId = param.AffId,
                UserId = param.UserId,
                Ip = param.Ip,
                NoteId = param.NoteId,
                NoteContent = param.NoteContent,
            };

            return ExecuteReturnResultInfo(command);
        }
    }
}
