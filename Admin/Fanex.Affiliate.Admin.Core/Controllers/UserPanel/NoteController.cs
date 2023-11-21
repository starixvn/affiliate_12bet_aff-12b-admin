namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    using System.Web.Mvc;
    using Fanex.Affiliate.Admin.Core.Attributes;
    using Fanex.Affiliate.Admin.Core.Constants;
    using Fanex.Affiliate.Admin.Core.MetaModels;
    using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
    using Fanex.Affiliate.Admin.Core.Services.Common;
    using Fanex.Affiliate.Admin.Core.Services.UserPanel;

    [FunctionAuthorize(FunctionName = Function.Note)]
    public class NoteController : NoteBaseController
    {
        public NoteController(
            ISessionService sessionService,
            INoteServiceBase noteService) : base(sessionService, noteService, NoteType.AffNote)
        { }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetNoteList(AffiliatePagingParam param)
        {
            return GetList(param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetNoteContentById(int affId, int noteId)
        {
            return GetContentById(affId, noteId);
        }

        [FunctionAuthorize(FunctionName = Function.Note, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult InsertNote(InsertUpdateNoteParam param)
        {
            return Insert(param);
        }

        [FunctionAuthorize(FunctionName = Function.Note, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult UpdateNote(InsertUpdateNoteParam param)
        {
            return Update(param);
        }
    }
}