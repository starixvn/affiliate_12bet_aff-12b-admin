namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    using Fanex.Affiliate.Admin.Core.Attributes;
    using Fanex.Affiliate.Admin.Core.Constants;
    using Fanex.Affiliate.Admin.Core.MetaModels;
    using MetaModels.UserPanel;
    using Services.Common;
    using Services.UserPanel;
    using System.Web.Mvc;

    [FunctionAuthorize(FunctionName = Function.MarketingTeamNote)]
    public class MarketingNoteController : NoteBaseController
    {
        public MarketingNoteController(
            ISessionService sessionService,
            INoteServiceBase noteService) : base(sessionService, noteService, NoteType.MarketingNote)
        { }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetMarketingNoteList(AffiliatePagingParam param)
        {
            return GetNoteList(param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetMarketingNoteContentById(int affId, int noteId)
        {
            return GetNoteContentById(affId, noteId);
        }

        [FunctionAuthorize(FunctionName = Function.MarketingTeamNote, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult InsertMarketingNote(InsertUpdateNoteParam param)
        {
            return InsertNote(param);
        }

        [FunctionAuthorize(FunctionName = Function.MarketingTeamNote, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult UpdateMarketingNote(InsertUpdateNoteParam param)
        {
            return UpdateNote(param);
        }
    }
}