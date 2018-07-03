using System;
using System.Linq;
using System.Web.Mvc;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Filters;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.ViewModels;
using ONEbook.UM.Filters;

namespace Fanex.Affiliate.User.Core.Controllers
{
    [Authenticate]
    [CheckUMFilter]
    public class MemberTransactionController : Controller
    {
        private readonly IMemberTransactionService _memberTransactionService;

        public MemberTransactionController(IMemberTransactionService memberTransactionService)
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

            return View(viewModel);
        }
    }
}