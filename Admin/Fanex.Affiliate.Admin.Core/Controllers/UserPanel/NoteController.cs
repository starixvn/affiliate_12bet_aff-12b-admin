using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels;

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

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult GetNoteList(AffiliatePagingParam param)
        {
            if (ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = _noteRepository.GetNoteList(param)
                };
            }

            return new JsonNetResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult GetNoteContentById(int affId, int noteId)
        {
            if (affId >= Validation.MinId && noteId >= Validation.MinId)
            {
                return new JsonNetResult
                {
                    Data = _noteRepository.GetNoteContentById(affId, noteId)
                };
            }

            return new JsonNetResult();
        }

        [FunctionAuthorize(FunctionName = Function.Affiliate, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult InsertNote(InsertUpdateNoteParam param)
        {
            if (ModelState.IsValid)
            {
                AssignCommonParam(param);

                var model = _noteRepository.InsertNote(param);

                return new JsonNetResult
                {
                    Data = model
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
        [Compress]
        public ActionResult UpdateNote(InsertUpdateNoteParam param)
        {
            if (ModelState.IsValid)
            {
                AssignCommonParam(param);

                var model = _noteRepository.UpdateNote(param);

                return new JsonNetResult
                {
                    Data = model
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

        private void AssignCommonParam(InsertUpdateNoteParam param)
        {
            param.UserId = SessionService.UserId;
            param.Ip = Request.UserHostAddress;
        }
    }
}
