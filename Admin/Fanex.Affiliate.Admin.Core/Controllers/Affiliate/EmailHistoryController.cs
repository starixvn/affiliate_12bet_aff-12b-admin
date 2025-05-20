using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.EmailHistory;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.EmailHistory;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.Affiliate
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class EmailHistoryController : ReadOnlySessionBaseController
    {
        private readonly IEmailHistoryService _emailHistoryService;

        public EmailHistoryController(
            ISessionService sessionService,
            IEmailHistoryService emailHistoryService) : base(sessionService)
        {
            _emailHistoryService = emailHistoryService;
        }

        public ActionResult Index()
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.AffiliateInfo, Url = Url.Content("~/Portal/Affiliate"), FunctionName = Function.Affiliate },
                new SubMenu { Name = Lang.PaymentRequest, Url = Url.Content("~/Portal/PaymentRequest"), FunctionName = Function.Payment },
                new SubMenu { Name = Lang.RevenueGroup, Url = Url.Content("~/Portal/RevenueGroup"), FunctionName = Function.RevenueGroup },
                new SubMenu { Name = Lang.EmailHistory, Url = Url.Content("~/Portal/EmailHistory"), FunctionName = Function.EmailHistory, IsActive = true },
                new SubMenu { Name = Lang.MarketingAccount, Url = Url.Content("~/Portal/MarketingAccount"), FunctionName = Function.Affiliate } // TODO: Need Review
            };

            return View("~/Areas/Portal/Views/Affiliate/EmailHistory.cshtml", model: subMenu);
        }

        [FunctionAuthorize(FunctionName = Function.EmailHistory)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        [UnauthorizedPropertyCensor]
        public ActionResult GetEmailHistoryList(EmailHistoryParam param)
        {
            if (ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = _emailHistoryService.GetEmailHistoryList(param)
                };
            }

            return new JsonNetResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.EmailHistory, IsHost = true)]
        public ActionResult GetEmailHistoryDetail(int selectedEmailId)
        {
            if (selectedEmailId >= Validation.MinId)
            {
                return new JsonNetResult
                {
                    Data = _emailHistoryService.GetEmailHistoryDetail(selectedEmailId)
                };
            }

            return new JsonNetResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.EmailHistory, IsHost = true)]
        public ActionResult DeleteEmailHistory(DeleteEmailHistoryParam param)
        {
            if (ModelState.IsValid)
            {
                AssignUserInfoParam(param);

                return new JsonNetResult
                {
                    Data = _emailHistoryService.DeleteEmailHistory(param)
                };
            }

            return GenerateModelStateErrResult();
        }
    }
}
