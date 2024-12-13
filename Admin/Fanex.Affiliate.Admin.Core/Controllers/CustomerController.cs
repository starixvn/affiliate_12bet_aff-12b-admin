using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Services.Affiliate;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.ViewModels.Customer;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class CustomerController : ReadOnlySessionBaseController
    {
        private readonly ISystemService _systemService;
        private readonly IAffTypeService _affTypeService;

        public CustomerController(
            ISessionService sessionService,
            ISystemService systemService,
            IAffTypeService affTypeService) : base(sessionService)
        {
            _systemService = systemService;
            _affTypeService = affTypeService;
        }

        public ActionResult Index(string user)
        {
            var viewModel = new CustomerViewModel
            {
                AffName = user,
                LanguageList = _systemService.GetLanguageList(),
                AffTypeList = _affTypeService.GetAffTypeList(),
                ProductList = _systemService.GetProductList(),
                CurrencyList = _systemService.GetCurrencies(),
                BaseCurrencyList = _systemService.GetBaseCurrencies()
            };

            if (string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(SessionService.AffName))
            {
                viewModel.AffName = SessionService.AffName;
            }

            return View(viewModel);
        }
    }
}