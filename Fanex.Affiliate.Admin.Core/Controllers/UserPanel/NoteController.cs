using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.ViewModels.UserPanel;
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
        public ActionResult Index(NoteParam param)
        {
            param.AffId = param.AffId == 0 ? SessionService.AffId : param.AffId;

            var model = _noteRepository.GetNoteList(param);

            var viewModel = new NoteViewModel
            {
                TotalItem = model.TotalItem,
                Param = param
            };
            
            return View("~/Areas/Portal/Views/UserPanel/Note/Index.cshtml", viewModel);
        }

        [HttpPost]
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
        public ActionResult InsertNote(InsertUpdateNoteParam param)
        {
            param.AffId = param.AffId == 0 ? SessionService.AffId : param.AffId;

            var model = _noteRepository.InsertNote(param);

            return new JsonNetResult
            {
                Data = model
            };
        }

        [HttpPost]
        public ActionResult UpdateNote(InsertUpdateNoteParam param)
        {
            param.AffId = param.AffId == 0 ? SessionService.AffId : param.AffId;

            var model = _noteRepository.UpdateNote(param);

            return new JsonNetResult
            {
                Data = model
            };
        }
    }
}
