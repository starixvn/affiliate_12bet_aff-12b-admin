using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;
using System.Web.Mvc;

namespace Fanex.Affiliate.User.Core.Controllers
{
    public class VerificationCodeController : BaseController
    {
        public VerificationCodeController(ISessionService sessionService, ILanguageService languageService) : base(sessionService, languageService)
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
