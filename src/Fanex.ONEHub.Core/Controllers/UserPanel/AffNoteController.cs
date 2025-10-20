using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class AffNoteController : NoteBaseController
    {
        public AffNoteController(
            ISessionService sessionService,
            INoteBaseService noteService) : base(sessionService, noteService, NoteSpPrefix.AffNote)
        { }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetAffNoteList(AffiliatePagingParam param)
        {
            return GetNoteList(param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Note)]
        public ActionResult GetAffNoteContentById(int affId, int noteId)
        {
            return GetNoteContentById(affId, noteId);
        }

        [FunctionAuthorize(FunctionName = Function.Note, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult InsertAffNote(InsertUpdateNoteParam param)
        {
            return InsertNote(param);
        }

        [FunctionAuthorize(FunctionName = Function.Note, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult UpdateAffNote(InsertUpdateNoteParam param)
        {
            return UpdateNote(param);
        }
    }
}
