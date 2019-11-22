using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class NoteController : BaseController
    {
        private readonly INoteService _noteService;

        public NoteController(
            ISessionService sessionService,
            INoteService noteService) : base(sessionService)
        {
            _noteService = noteService;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetNoteList(AffiliatePagingParam param)
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

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetNoteContentById(int affId, int noteId)
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

        [FunctionAuthorize(FunctionName = Function.Affiliate, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult InsertNote(InsertUpdateNoteParam param)
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

        [FunctionAuthorize(FunctionName = Function.Affiliate, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult UpdateNote(InsertUpdateNoteParam param)
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
