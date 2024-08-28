namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    using Fanex.Affiliate.Admin.Core.Attributes;
    using Fanex.Affiliate.Admin.Core.Constants;
    using Fanex.Affiliate.Admin.Core.DTO;
    using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
    using Fanex.Affiliate.Admin.Core.Extensions;
    using Fanex.Affiliate.Admin.Core.MetaModels;
    using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
    using Fanex.Affiliate.Admin.Core.Services.Common;
    using Fanex.Affiliate.Admin.Core.Services.UserPanel;
    using System.Web.Mvc;

    [FunctionAuthorize(FunctionName = Function.CustomerMenu)]
    public class NoteBaseController : ReadOnlySessionBaseController
    {
        private readonly INoteServiceBase _noteService;

        public NoteBaseController(
            ISessionService sessionService,
            INoteServiceBase noteService,
            string noteType) : base(sessionService)
        {
            _noteService = noteService;
            _noteService.FunctionName = noteType;
        }

        protected ActionResult GetNoteList(AffiliatePagingParam param)
        {
            if (ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = _noteService.GetNoteList(param)
                };
            }

            return new JsonNetResult();
        }

        protected ActionResult GetNoteContentById(int affId, int noteId)
        {
            if (affId >= Validation.MinId && noteId >= Validation.MinId)
            {
                var noteInfo = _noteService.GetNoteContentById(affId, noteId);
                return new JsonNetResult
                {
                    Data = noteInfo != null ? new Note
                    {
                        NoteId = noteInfo.NoteId,
                        NoteContent = Server.HtmlDecode(noteInfo.NoteContent)
                    } : null
                };
            }

            return new JsonNetResult();
        }

        protected ActionResult InsertNote(InsertUpdateNoteParam param)
        {
            if (ModelState.IsValid)
            {
                var insertNoteParam = AssignCommonParam(param);

                return new JsonNetResult
                {
                    Data = _noteService.InsertNote(insertNoteParam)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "Insert Note is failed!"
                }
            };
        }

        protected ActionResult UpdateNote(InsertUpdateNoteParam param)
        {
            if (ModelState.IsValid && param.NoteId >= Validation.MinId)
            {
                var updateNoteParam = AssignCommonParam(param);

                return new JsonNetResult
                {
                    Data = _noteService.UpdateNote(updateNoteParam)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "Update Note is failed!"
                }
            };
        }

        private InsertUpdateNoteParam AssignCommonParam(InsertUpdateNoteParam param)
        {
            return new InsertUpdateNoteParam
            {
                AffId = param.AffId,
                UserId = SessionService.UserId,
                Ip = Request.UserHostAddress,
                NoteId = param.NoteId,
                NoteContent = Server.HtmlEncode(param.NoteContent)
            };
        }
    }
}