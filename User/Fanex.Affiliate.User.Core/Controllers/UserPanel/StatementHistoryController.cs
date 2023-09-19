namespace Fanex.Affiliate.User.Core.Controllers
{
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.Constants;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.DTO.UserPanel;
    using Fanex.Affiliate.User.Core.Repository.UserPanel;
    using Fanex.Affiliate.User.Core.Utilities;
    using Fanex.Affiliate.User.Core.ViewModels.UserPanel;
    using Filters;
    using MetalModels.UserPanel;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    [CustomAuthorize]
    public class StatementHistoryController : BaseController
    {
        private const int DefaultPageSize = 20;
        private readonly IAppSetting _appSetting;
        private readonly IStatementHistoryRepository _statementHistoryRepository;

        public StatementHistoryController(ISessionService sessionService,
            ILanguageService languageService,
            IAppSetting appSetting,
            IStatementHistoryRepository statementHistoryRepository

            ) : base(sessionService, languageService)
        {
            _appSetting = appSetting;
            _statementHistoryRepository = statementHistoryRepository;
        }
        [HttpGet]
        [AjaxOnly]
        public ActionResult Index(bool isClickBreadcumb = false)
        {
            var param = new StatementHistoryParam
            {
                AffId = SessionService.UserSession.AffId,
                UserId = SessionService.UserSession.UserId,
                IsInternal = SessionService.IsInternal,
                PageSize = DefaultPageSize
            };

            var statementHistoryList = GetStatementHistoryViewModel(param);
            ViewBag.TotalItem = param.NRows;
            ViewBag.StartRowNumber = 1;
            ViewBag.AvailableFromDate = _appSetting.StatementAvailableFrom;
            ViewBag.IsClickBreadcumb = isClickBreadcumb.ToString();

            return View("~/Views/UserPanel/StatementHistory/Index.cshtml", statementHistoryList);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Index(StatementHistoryParam param)
        {
            param.AffId = SessionService.UserSession.AffId;
            param.UserId = SessionService.UserSession.UserId;
            param.IsInternal = SessionService.IsInternal;
            param.PageSize = param.PageSize ?? DefaultPageSize;

            var statementHistoryList = GetStatementHistoryViewModel(param);
            ViewBag.TotalItem = param.NRows;
            ViewBag.StartRowNumber = (param.PageIndex - 1) * param.PageSize + 1;
            ViewBag.AvailableFromDate = _appSetting.StatementAvailableFrom;
            return PartialView("~/Views/UserPanel/StatementHistory/StatementHistoryPartial/_StatementHistoryContent.cshtml", statementHistoryList);
        }

        private IEnumerable<StatementHistoryViewModel> GetStatementHistoryViewModel(StatementHistoryParam param)
        {
            var statementHistoryViews = new List<StatementHistoryViewModel>();
            var statementHistoryList = _statementHistoryRepository.GetStatementHistory(param);

            foreach (var item in statementHistoryList)
            {
                if (item.TransType == TransType.Commission || item.TransType == TransType.NewFDM)
                {
                    if (CheckIfExistMonthlyTotal(item.MonthDate, statementHistoryViews))
                    {
                        continue;
                    }
                    statementHistoryViews.Add(CreateMonthlyTotal(item.MonthDate, statementHistoryList));
                }
                else
                {
                    statementHistoryViews.Add(CreateViewModelFromStatementHistoryItem(item));
                }
            }

            return statementHistoryViews;
        }

        private static StatementHistoryViewModel CreateMonthlyTotal(DateTime monthDate, IEnumerable<StatementHistory> statementHistoryList)
        {
            var transItems = statementHistoryList.Where(s => s.MonthDate == monthDate)
                .Where(s => s.TransType == TransType.NewFDM || s.TransType == TransType.Commission);

            var monthlyCom = transItems.FirstOrDefault(s => s.TransType == TransType.Commission);
            var children = transItems.Select(s => CreateViewModelFromStatementHistoryItem(s));

            var monthlyTotal = new StatementHistoryViewModel
            {
                Amount = transItems.Sum(s => s.Amount),
                MonthDate = monthDate,
                Children = children,
                Status = monthlyCom?.Status ?? children.FirstOrDefault().Status,
                TransType = TransType.MonthlyTotal,
                CalculatedType = monthlyCom?.CalculatedType ?? children.FirstOrDefault().CalculatedType
            };
            return monthlyTotal;
        }

        private static bool CheckIfExistMonthlyTotal(DateTime monthDate, List<StatementHistoryViewModel> statementHistoryViews)
        {
            return statementHistoryViews.Any(m => m.MonthDate == monthDate && m.TransType == TransType.MonthlyTotal);
        }

        private static StatementHistoryViewModel CreateViewModelFromStatementHistoryItem(StatementHistory item)
        {
            return new StatementHistoryViewModel
            {
                Amount = item.Amount,
                Description = item.Description,
                MonthDate = item.MonthDate,
                Status = item.Status,
                TransType = item.TransType,
                CalculatedType = item.CalculatedType
            };
        }
    }
}