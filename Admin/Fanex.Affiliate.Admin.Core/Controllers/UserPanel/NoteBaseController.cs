using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class NoteBaseController : ReadOnlySessionBaseController
    {
        private readonly INoteBaseService _noteBaseService;

        public NoteBaseController(
            ISessionService sessionService,
            INoteBaseService noteBaseService,
            string spPrefix) : base(sessionService)
        {
            _noteBaseService = noteBaseService;
            _noteBaseService.SpPrefix = spPrefix;
        }

        protected ActionResult GetNoteList(AffiliatePagingParam param)
        {
            if (ModelState.IsValid)
            {
                return GenerateJsonNetResult(_noteBaseService.GetNoteList(param));
            }

            return new JsonNetResult();
        }

        protected ActionResult GetNoteContentById(int affId, int noteId)
        {
            if (affId >= Validation.MinId && noteId >= Validation.MinId)
            {
                var noteInfo = _noteBaseService.GetNoteContentById(affId, noteId);
                var decodedNoteInfo = noteInfo != null ? new NoteBasicInfo { NoteId = noteInfo.NoteId, NoteContent = Server.HtmlDecode(noteInfo.NoteContent) } : null;
                
                return GenerateJsonNetResult(decodedNoteInfo);
            }

            return new JsonNetResult();
        }

        protected ActionResult InsertNote(InsertUpdateNoteParam param)
        {
            if (ModelState.IsValid)
            {
                var insertNoteParam = AssignCommonParam(param);

                return GenerateJsonNetResult(_noteBaseService.InsertNote(insertNoteParam));
            }

            return GenerateErrResult("Insert Note is failed!");
        }

        protected ActionResult UpdateNote(InsertUpdateNoteParam param)
        {
            if (ModelState.IsValid && param.NoteId >= Validation.MinId)
            {
                var updateNoteParam = AssignCommonParam(param);

                return GenerateJsonNetResult(_noteBaseService.UpdateNote(updateNoteParam));
            }

            return GenerateErrResult("Update Note is failed!");
        }

        private InsertUpdateNoteParam AssignCommonParam(InsertUpdateNoteParam param)
        {
            var noteParam = new InsertUpdateNoteParam
            {
                AffId = param.AffId,
                NoteId = param.NoteId,
                NoteContent = Server.HtmlEncode(param.NoteContent)
            };
            AssignUserInfoParam(noteParam);

            return noteParam;
        }
    }
}
