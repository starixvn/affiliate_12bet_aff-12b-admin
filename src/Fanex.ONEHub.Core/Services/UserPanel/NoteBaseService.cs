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
    public class NoteBaseService : BaseDynamicService, INoteBaseService
    {
        public string SpPrefix { get; set; }

        public NoteBaseService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

        public EntityResult<NoteInfo> GetNoteList(AffiliatePagingParam param)
        {
            var criteria = new NoteCriteria(SpPrefix)
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
            var command = new InsertNoteCommand(SpPrefix);
            AssignNoteCommand(command, param);

            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo UpdateNote(InsertUpdateNoteParam param)
        {
            var command = new UpdateNoteCommand(SpPrefix, param.NoteId);
            AssignNoteCommand(command, param);

            return ExecuteReturnResultInfo(command);
        }

        private static void AssignNoteCommand(InsertNoteCommand command, InsertUpdateNoteParam param)
        {
            command.AffId = param.AffId;
            command.UserId = param.UserId;
            command.Ip = param.Ip;
            command.NoteContent = param.NoteContent;
        }
    }
}
