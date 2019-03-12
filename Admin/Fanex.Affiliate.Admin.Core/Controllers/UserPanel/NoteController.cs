using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
using Fanex.Affiliate.Admin.Core.Extensions;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class NoteController : BaseController
    {
        private readonly INoteRepository _noteRepository;
        public NoteController(
            ISessionService sessionService,
            INoteRepository noteRepository) : base(sessionService)
        {
            _noteRepository = noteRepository;
        }

        [HttpGet]
        public ActionResult Index(AffiliateLinkParam param)
        {
            param.AffId = param.AffId == 0 ? SessionService.AffId : param.AffId;
            
            return View("~/Areas/Portal/Views/UserPanel/Note/Index.cshtml", param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult GetNoteList(NoteParam param)
        {
            param.AffId = param.AffId == 0 ? SessionService.AffId : param.AffId;

            var model = _noteRepository.GetNoteList(param);

            return new JsonNetResult
            {
                Data = model
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult GetNoteContentById(int affId, int noteId)
        {
            return new JsonNetResult
            {
                Data = _noteRepository.GetNoteContentById(affId == 0 ? SessionService.AffId : affId, noteId)
            };
        }

        [FunctionAuthorize(FunctionName = Function.Affiliate, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult InsertNote(InsertUpdateNoteParam param)
        {
            AssignCommonParam(param);

            var model = _noteRepository.InsertNote(param);

            return new JsonNetResult
            {
                Data = model
            };
        }

        [FunctionAuthorize(FunctionName = Function.Affiliate, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult UpdateNote(InsertUpdateNoteParam param)
        {
            AssignCommonParam(param);

            var model = _noteRepository.UpdateNote(param);

            return new JsonNetResult
            {
                Data = model
            };
        }

        private void AssignCommonParam(InsertUpdateNoteParam param)
        {
            param.AffId = param.AffId == 0 ? SessionService.AffId : param.AffId;
            param.UserId = SessionService.UserId;
            param.Ip = Request.UserHostAddress;
        }
    }
}
