using System;
using System.Web;
using Affiliate.Infrastructure.Helpers;
using Affiliate.MailServices;
using DNAide.Core;

namespace AffiliateUser.Business.Services
{
    public class RequestPayoutService
    {
        private const int MinAvailableDay = 8;

        private const int MaxAvailableDay = 15;

        public void RequestPayout(HttpContext context)
        {
            context = AffHandler.InitializeCulture(context);
            context.Response.ContentType = "text/html";
            try
            {
                AntiForgeryHelper.ValidateXsrfToken(context);
            }
            catch
            {
                context.Response.Write("<script type='text/javascript'>parent.$(\"errMsg\").show();parent.Common.showError([\"" + Resources.Lang.ErrAuthorized + "\"], [\"error_item\"], \"error_item\");</script>");
                return;
            }

            string strPreferredLang = "E";
            if (AppSettings.Session.IsInternalSite)
            {
                context.Response.Write("<script type='text/javascript'>parent.$(\"errMsg\").show();parent.Common.showError([\"" + Resources.Lang.ErrAuthorized + "\"], [\"error_item\"], \"error_item\");</script>");
                return;
            }
            try
            {
                int intErrCode = -1;
                decimal Amt = 0;
                Int64 intTransId = 0;
                string strSuccMsg = string.Empty;
                string strErrorMsg = string.Empty;
                DateTime dtmWinlostDate = new DateTime();
                if (!string.IsNullOrEmpty(context.Request.Form["action"]) && context.Request.Form["action"] == "cancel")//Cacel request
                {
                    intTransId = CastAide.AsInt64(context.Request.Form["transid"]);
                    intErrCode = DataHandler.PaymentRequest.CancelRequest(intTransId, AppSettings.Session.Language, ref strPreferredLang, ref strErrorMsg);
                    strSuccMsg = Resources.Lang.CancelPayoutSucc;
                    Amt = CastAide.AsDecimal(context.Request.Form["amt"]);
                }
                else
                {
                    if ((DateTime.Now.Day < MinAvailableDay || DateTime.Now.Day > MaxAvailableDay) && !AppSettings.LocalTest)
                    {
                        context.Response.Write("<script type='text/javascript'>parent.ShowError1(\"" + Resources.Lang.PayoutTime + "\");</script>");
                        return;
                    }
                    if (string.IsNullOrEmpty(context.Request.Form["txtAmt"]))
                        context.Response.Write("<script type='text/javascript'>parent.Common.displayMessage(false);</script>");
                    Amt = CastAide.AsDecimal(context.Request.Form["txtAmt"]);
                    if (Amt == 0)
                    {
                        context.Response.Write("<script type='text/javascript'>parent.Common.displayMessage(false);</script>");
                        return;
                    }
                    else
                    {
                        #region Activate 12Bet Player Account if not

                        var isActive = Active12BetAccount.Check12BetUsernameByAffId(AppSettings.Session.CustID, ref strErrorMsg);
                        if (!isActive)
                        {
                            context.Response.Write("<script type='text/javascript'>parent.ShowError1(\"" + strErrorMsg + "\");</script>");
                            return;
                        }

                        #endregion Activate 12Bet Player Account if not

                        intErrCode = DataHandler.PaymentRequest.PayoutRequest(Amt, AppSettings.Session.CustID, ref intTransId, ref dtmWinlostDate, ref strPreferredLang, ref strErrorMsg);
                        strSuccMsg = Resources.Lang.PayoutSuccess;
                    }
                }
                if (intErrCode != 0)
                {
                    context.Response.Write("<script type='text/javascript'>parent.ShowError1(\"" + Common.HTMLEncode(strErrorMsg) + "\");</script>");
                }
                else
                {
                    string strBalance = string.Empty;
                    string strMinAmt = string.Empty;
                    string strPaymentMethod = string.Empty;
                    string strListPayment = GenerateHTML.PaymentRequest.PaymentList(ref strBalance, ref strMinAmt, ref strPaymentMethod);

                    var mailService = new RequestPayoutMailService
                    {
                        Action = context.Request["action"],
                        Date = context.Request["date"],
                        Amt = Amt,
                        TransId = intTransId,
                        PreferredLanguage = strPreferredLang,
                        DtmWinlostDate = dtmWinlostDate
                    };

                    mailService.SendRequestPayoutMail();

                    context.Response.Write("<script type='text/javascript'>parent.UpdateSuccess(\"" + strSuccMsg + "\", \"" + strBalance + "\", \"" + strMinAmt + "\", \"" + strListPayment.Replace("\r\n", "").Replace("\"", "'") + "\");</script>");
                }
            }
            catch (System.ServiceModel.EndpointNotFoundException endpointNotFoundException)
            {
                context.Response.Write("<script type='text/javascript'>parent.ShowError1('" + Common.HTMLEncode(Resources.Lang.MailServerNotAvailable) + "');</script>");
                Fanex.Logging.LogService.Error(endpointNotFoundException.Message);
            }
            catch (Exception ex)
            {
                context.Response.Write("<script type='text/javascript'>parent.ShowError1('" + Common.HTMLEncode(ex.Message) + "');</script>");
                Fanex.Logging.LogService.Error(ex.Message);
            }
        }
    }
}