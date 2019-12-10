using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.ViewModels.Customer;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class CustomerController : BaseController
    {
        private readonly ILanguageService _languageService;

        public CustomerController(
            ISessionService sessionService,
            ILanguageService languageService) : base(sessionService)
        {
            _languageService = languageService;
        }

        public ActionResult Index(string user)
        {
            var viewModel = new CustomerViewModel
            {
                AffName = user,
                LanguageList = _languageService.GetLanguageList()
            };

            if (string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(SessionService.AffName))
            {
                viewModel.AffName = SessionService.AffName;
            }

            return View(viewModel);
        }
    }
}