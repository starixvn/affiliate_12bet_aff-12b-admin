using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    [CustomAuthorize]
    public class OffersForMeController : BaseController
    {
        private readonly IOfferService _offerService;

        public OffersForMeController(
            IOfferService offerService,
            ISessionService sessionService,
            ILanguageService languageService)
            : base(sessionService, languageService)
        {
            _offerService = offerService;
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.Content = await _offerService.GetOfferById(SessionService.UserSession.OfferPageId);
            return View("~/Views/UserPanel/OffersForMe/Index.cshtml");
        }

        [AllowAnonymous]
        public async Task<ActionResult> Preview(int id)
        {
            ViewBag.Content = await _offerService.GetOfferById(id);
            return View("~/Views/UserPanel/OffersForMe/Preview.cshtml");
        }
    }
}
