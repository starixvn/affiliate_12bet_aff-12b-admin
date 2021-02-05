using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.ViewModels.Customer;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.CustomerMenu)]
    public class CustomerController : ReadOnlySessionBaseController
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
                LanguageList = _languageService.GetInChangeMarkets()
            };

            if (string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(SessionService.AffName))
            {
                viewModel.AffName = SessionService.AffName;
            }

            return View(viewModel);
        }
    }
}