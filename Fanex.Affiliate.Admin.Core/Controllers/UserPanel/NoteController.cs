using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;

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
            return View("~/Areas/Portal/Views/UserPanel/Note/Index.cshtml");
        }
    }
}
