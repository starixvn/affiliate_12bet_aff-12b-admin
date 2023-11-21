namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    using System.Web.Mvc;
    using Fanex.Affiliate.Admin.Core.Attributes;
    using Fanex.Affiliate.Admin.Core.Constants;
    using Fanex.Affiliate.Admin.Core.MetaModels;
    using MetaModels.UserPanel;
    using Services.Common;
    using Services.UserPanel;

    [FunctionAuthorize(FunctionName = Function.MarketingTeamNote)]
    public class MarketingNoteController : NoteBaseController
    {
        public MarketingNoteController(
            ISessionService sessionService,
            INoteServiceBase noteService) : base(sessionService, noteService, NoteType.MarketingNote) 
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

        [FunctionAuthorize(FunctionName = Function.MarketingTeamNote, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult InsertNote(InsertUpdateNoteParam param)
        {
            return Insert(param);
        }

        [FunctionAuthorize(FunctionName = Function.MarketingTeamNote, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult UpdateNote(InsertUpdateNoteParam param)
        {
            return Update(param);
        }
    }
}