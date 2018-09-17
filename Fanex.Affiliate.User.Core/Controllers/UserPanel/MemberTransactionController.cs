using System;
using System.Linq;
using System.Web.Mvc;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.Filters;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.ViewModels;

namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    [CustomAuthorize]
    public class MemberTransactionController : BaseController
    {
        private readonly IMemberTransactionService _memberTransactionService;

        public MemberTransactionController(ISessionService sessionService,
            ILanguageService languageService, IMemberTransactionService memberTransactionService)
            : base(sessionService, languageService)
        {
            _memberTransactionService = memberTransactionService;
        }

        public ActionResult Index(MemberTransactionParam param)
        {
            if (!param.Date.HasValue)
            {
                param.Date = DateTime.Now;
            }

            var transactionList = _memberTransactionService.GetTransactionInfo(param);

            var viewModel = new MemberTransactionViewModel
            {
                TransactionList = transactionList,
                Total = transactionList.Sum(t => t.Amount)
            };

            return View("~/Views/UserPanel/MemberTransaction/Index.cshtml", viewModel);
        }
    }
}