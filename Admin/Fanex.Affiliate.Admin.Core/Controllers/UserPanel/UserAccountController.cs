using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    [MultiFunctionAuthorize(FunctionName = Function.EditAffiliate, FunctionNameOption = Function.ManagePendingAff)]
    public class UserAccountController : ReadOnlySessionBaseController
    {
        private readonly IUserAccountService _userAccountService;

        private const string AdminPageDefaultLang = "en-US";

        public UserAccountController(
            ISessionService sessionService,
            IUserAccountService userAccountService) : base(sessionService)
        {
            _userAccountService = userAccountService;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [UpdateUnauthorizedProperty]
        public ActionResult GetUserAccountInfo(int affId)
        {
            var userAccountInfo = _userAccountService.GetUserAccountInfo(affId);
            var securityQuestions = (SessionService.UserClaim.HasClaim(Function.SecurityQA) || (userAccountInfo.IsPendingAff && SessionService.UserClaim.HasClaim(Function.ManagePendingAff)))
                ? _userAccountService.GetSecurityQuestions(AdminPageDefaultLang).Select(q => new KeyValuePair<int, string>(q.QuestionId, q.Question))
                : null;

            if (userAccountInfo != null)
            {
                var newUserAccountInfo = new UserAccount
                {
                    AffId = userAccountInfo.AffId,
                    AffName = userAccountInfo.AffName,
                    Status = Server.HtmlDecode(userAccountInfo.Status),
                    FirstName = Server.HtmlDecode(userAccountInfo.FirstName),
                    LastName = Server.HtmlDecode(userAccountInfo.LastName),
                    Gender = userAccountInfo.Gender,
                    Email = Server.HtmlDecode(userAccountInfo.Email),
                    Phone = Server.HtmlDecode(userAccountInfo.Phone),
                    ContactApps = Server.HtmlDecode(userAccountInfo.ContactApps),
                    Address = Server.HtmlDecode(userAccountInfo.Address),
                    City = Server.HtmlDecode(userAccountInfo.City),
                    State = Server.HtmlDecode(userAccountInfo.State),
                    Postcode = Server.HtmlDecode(userAccountInfo.Postcode),
                    Country = Server.HtmlDecode(userAccountInfo.Country),
                    SiteUrl = Server.HtmlDecode(userAccountInfo.SiteUrl),
                    MoreSiteUrls = Server.HtmlDecode(userAccountInfo.MoreSiteUrls),
                    NoSiteDescription = Server.HtmlDecode(userAccountInfo.NoSiteDescription),
                    PreferredPayment = Server.HtmlDecode(userAccountInfo.PreferredPayment),
                    PlayerUsername = Server.HtmlDecode(userAccountInfo.PlayerUsername),
                    NetellerAccountId = Server.HtmlDecode(userAccountInfo.NetellerAccountId),
                    MoneybookersEmail = Server.HtmlDecode(userAccountInfo.MoneybookersEmail),
                    BankName = Server.HtmlDecode(userAccountInfo.BankName),
                    BankAddress = Server.HtmlDecode(userAccountInfo.BankAddress),
                    BankCode = Server.HtmlDecode(userAccountInfo.BankCode),
                    BankAccountName = Server.HtmlDecode(userAccountInfo.BankAccountName),
                    BankAccountNumber = Server.HtmlDecode(userAccountInfo.BankAccountNumber),
                    Question = Server.HtmlDecode(userAccountInfo.Question),
                    QuestionId = userAccountInfo.QuestionId,
                    Answer = Server.HtmlDecode(userAccountInfo.Answer),
                    MailOption = userAccountInfo.MailOption,
                    SecurityQuestions = securityQuestions
                };

                if (!string.IsNullOrEmpty(newUserAccountInfo.ContactApps))
                {
                    newUserAccountInfo.ContactAppList = JsonConvert.DeserializeObject<IEnumerable<ContactApp>>(newUserAccountInfo.ContactApps).OrderBy(p => p.Order);
                }

                return new JsonNetResult
                {
                    Data = newUserAccountInfo
                };
            }

            return new JsonNetResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [MultiFunctionAuthorize(FunctionName = Function.EditAffiliate, FunctionNameOption = Function.ManagePendingAff, IsHost = true)]
        public ActionResult UpdateUserAccount(UserAccountParam param)
        {
            if (ModelState.IsValid)
            {
                var userAccountParam = new UserAccountParam
                {
                    IsPendingAff = param.IsPendingAff,
                    AffId = param.AffId,
                    FirstName = Server.HtmlEncode(param.FirstName),
                    LastName = Server.HtmlEncode(param.LastName),
                    Gender = param.Gender,
                    Email = Server.HtmlEncode(param.Email),
                    Phone = Server.HtmlEncode(param.Phone),
                    Address = Server.HtmlEncode(param.Address),
                    City = Server.HtmlEncode(param.City),
                    State = Server.HtmlEncode(param.State),
                    Postcode = Server.HtmlEncode(param.Postcode),
                    Country = Server.HtmlEncode(param.Country),
                    SiteUrl = Server.HtmlEncode(param.SiteUrl),
                    MoreSiteUrls = Server.HtmlEncode(param.MoreSiteUrls),
                    NoSiteDescription = Server.HtmlEncode(param.NoSiteDescription),
                    PreferredPayment = Server.HtmlEncode(param.PreferredPayment),
                    PlayerUsername = Server.HtmlEncode(param.PlayerUsername),
                    NetellerAccountId = Server.HtmlEncode(param.NetellerAccountId),
                    MoneybookersEmail = Server.HtmlEncode(param.MoneybookersEmail),
                    BankName = Server.HtmlEncode(param.BankName),
                    BankAddress = Server.HtmlEncode(param.BankAddress),
                    BankCode = Server.HtmlEncode(param.BankCode),
                    BankAccountName = Server.HtmlEncode(param.BankAccountName),
                    BankAccountNumber = Server.HtmlEncode(param.BankAccountNumber),
                    MailOption = param.MailOption,
                    QuestionId = param.QuestionId,
                    Answer = param.Answer,
                    HasWebSite = param.HasWebSite
                };
                AssignUserInfoParam(userAccountParam);

                if (param.ContactAppList == null || param.ContactAppList.All(c => string.IsNullOrEmpty(c.Value)))
                {
                    return new JsonNetResult
                    {
                        Data = ResultInfo.GenerateErr("At least one IM account is required!")
                    };
                }
                else
                {
                    userAccountParam.ContactApps = JsonConvert.SerializeObject(param.ContactAppList.Select(p => new ContactApp
                    {
                        Name = Server.HtmlEncode(p.Name),
                        Value = Server.HtmlEncode(p.Value),
                        Order = p.Order
                    }));
                }

                return new JsonNetResult
                {
                    Data = _userAccountService.UpdateUserAccount(userAccountParam)
                };
            }
            return ModelState.GenerateErrorResult();
        }
    }
}
