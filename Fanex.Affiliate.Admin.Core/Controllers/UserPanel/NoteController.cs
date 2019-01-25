using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.ViewModels.UserPanel;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class NoteController : BaseController
    {
        public NoteController(ISessionService sessionService) : base(sessionService)
        {
        }

        [HttpGet]
        public ActionResult Index(NoteParam param)
        {
            var note1 = new Note
            {
                NoteId = 1,
                Content = "abc",
                CreatedBy = "ABC",
                LastModify = DateTime.Now
            };
            var note2 = new Note
            {
                NoteId = 2,
                Content = "xyz",
                CreatedBy = "XYZ",
                LastModify = DateTime.Now
            };
            var dataList = new List<Note>();
            dataList.Add(note1);
            dataList.Add(note2);

            var viewModel = new NoteViewModel
            {
                NoteList = dataList,
                Param = param
            };
            
            return View("~/Areas/Portal/Views/UserPanel/Note/Index.cshtml", viewModel);
        }
    }
}
