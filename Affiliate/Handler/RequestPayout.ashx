<%@ WebHandler Language="C#" Class="RequestPayout" %>

using System;
using System.Web;
using System.Web.SessionState;
using Affiliate.MailServices;
using DNAide.Core;
using DNAide.Data;
using DNAide.Web;
using DNAide.Utils;

public class RequestPayout : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context = AffHandler.InitializeCulture(context);
        context.Response.ContentType = "text/html";
        string strPreferredLang = "E";
        if (AppSettings.Session.IsInternalSite)
        {
            context.Response.Write("<script type='text/javascript'>parent.$(\"errMsg\").show();parent.ShowError([\"" + Resources.Lang.ErrAuthorized + "\"], [\"error_item\"], \"error_item\");</script>");
            return;
        }
        try
        {
            int intErrCode = -1;
            decimal Amt = 0;
            Int64 intTransId = 0;
            //string strErrObj = "error_item";
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
            else//Submit request
            {
                if (!(DateTime.Now.Day >= 1 && DateTime.Now.Day <= 30))
                {
                    context.Response.Write("<script type='text/javascript'>parent.ShowError1(\"" + Resources.Lang.PayoutTime + "\");</script>");
                    return;
                }
                if (string.IsNullOrEmpty(context.Request.Form["txtAmt"]))
                    context.Response.Write("<script type='text/javascript'>parent.DisplayMsg(false);</script>");
                Amt = CastAide.AsDecimal(context.Request.Form["txtAmt"]);
                if (Amt == 0)
                {
                    context.Response.Write("<script type='text/javascript'>parent.DisplayMsg(false);</script>");
                    return;
                }
                else
                {
                    #region Activate 12Bet Player Account if not
                    Boolean isActive = Active12BetAccount.Check12BetUsernameByAffId(AppSettings.Session.CustID, ref strErrorMsg);
                    if (!isActive)
                    {
                        context.Response.Write("<script type='text/javascript'>parent.ShowError1(\"" + strErrorMsg + "\");</script>");
                        return;
                    }
                    #endregion
                    intErrCode = DataHandler.PaymentRequest.PayoutRequest(Amt, AppSettings.Session.CustID,  ref intTransId, ref dtmWinlostDate, ref strPreferredLang, ref strErrorMsg);
                    strSuccMsg = Resources.Lang.PayoutSuccess;
                   // strErrObj = "txtAmt";
                    //intErrCode = CastAide.AsInt32(result.Outputs["errcode"]);                    
                }
            }
            if (intErrCode != 0)
            {
                /*if(intErrCode != 3005)//Error 3005 must get from dateabase
                    strErrorMsg = Common.GetErrMsg(intErrCode, ref strErrObj);*/
                context.Response.Write("<script type='text/javascript'>parent.ShowError1(\"" + Common.HTMLEncode(strErrorMsg) + "\");</script>");
            }
            else
            {
                string strBalance = string.Empty;
                string strMinAmt = string.Empty;
                string strPaymentMethod=string.Empty;
                string strListPayment = GenerateHTML.PaymentRequest.PaymentList(ref strBalance, ref strMinAmt,ref strPaymentMethod);

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
        catch (Exception ex)
        {            
            context.Response.Write("<script type='text/javascript'>parent.ShowError1('" + Common.HTMLEncode(ex.Message) + "');</script>");
            DNAide.Web.DNAideLog.Write(ex.Message);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}