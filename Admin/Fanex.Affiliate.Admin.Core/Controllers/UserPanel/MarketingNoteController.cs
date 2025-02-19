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
    public class MarketingNoteController : NoteBaseController
    {
        public MarketingNoteController(
            ISessionService sessionService,
            INoteBaseService noteService) : base(sessionService, noteService, NoteSpPrefix.MarketingNote)
        { }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetMarketingNoteList(AffiliatePagingParam param)
        {
            return GetNoteList(param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.MarketingTeamNote)]
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
