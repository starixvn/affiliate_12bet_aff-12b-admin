using System;
using System.Web;
using DNAide.Core;
using DNAide.Utils;
using DNAide.Web;

namespace AffiliateAdmin.Business.Services
{
    public class PaymentUpdateService
    {
        public void PaymentUpdate(HttpContext context)
        {
            context = IBCHandler.InitializeCulture(context);
            try
            {
                if (!string.IsNullOrEmpty(context.Request["hdApprove"]))
                {
                    string strActionType = context.Request["hdApprove"];
                    string strAdminRemark = context.Request["txtAdminRemark"];
                    string strAffRemark = context.Request["txtAffRemark"];
                    string strPreferredLang = "en-US";
                    // int AffId = CastAide.AsInt32(context.Request["hdCustId"]);
                    Int64 intTransId = CastAide.AsInt64(context.Request["hdTransId"]);
                    int intErrCode = -1;
                    bool blMailOption = false;
                    if (!(strActionType != Common.Approved || strActionType != Common.Rejected))
                        throw new Exception(Resources.Lang.ApprovedErr);

                    intErrCode = DataHandler.PaymentRequest.PaymentUpdate(intTransId, strActionType, Common.GetLangChar(), strAdminRemark, strAffRemark, ref strPreferredLang, ref blMailOption);

                    if (intErrCode == 0)
                    {
                        string strMsgMail = string.Empty;
                        //Send Email
                        string strFName = context.Request["fname"];
                        string strLName = context.Request["lname"];
                        string strEmail = context.Request["hdEmail"];
                        string strMsg = Resources.Lang.TransactionsId + " " + intTransId + " " + Resources.Lang.PaymentSuccess;
                        string strEmailSuffix = Common.EmailSuffix(blMailOption);
                        decimal Amt = CastAide.AsDecimal(context.Request["hdAmt"].Replace("$", string.Empty));

                        DateTime dtmWLDate = CastAide.AsDateTime(context.Request["wldate"]);
                        if (strActionType == Common.Approved)
                            ApprovedMail(intTransId, strFName, strLName, Amt, dtmWLDate, strEmail, strPreferredLang, strEmailSuffix);
                        else
                            CancelMail(intTransId, strFName, strLName, Amt, dtmWLDate, strEmail, strPreferredLang, strEmailSuffix);

                        context.Response.Write("<script type='text/javascript'>parent.UpdateSuccess('" + strMsg + "');</script>");
                    }
                    else
                    {
                        string strErrObj = "error_item";
                        string strMsg = GetErrMsg(intErrCode, ref strErrObj);
                        context.Response.Write("<script type='text/javascript'>parent.UpdateErr(\"" + strMsg + "\");</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("<script type='text/javascript'>parent.UpdateErr(\"" + ex.Message + "\");</script>");
                Fanex.Logging.LogService.Error(ex.Message);
            }
        }

        private void CancelMail(Int64 intTransId, string strFName, string strLName, decimal Amt, DateTime dtmWLDate, string strEmail, string strPreferredLang, string strEmailSuffix)
        {
            string FileName = "Email/PaymentCan/PaymentCanEmail_" + strPreferredLang + strEmailSuffix + ".htm";
            string strNoticeMail = TemplateHandler.LoadTemplate(FileName).Replace("\r\n", "");
            string strURLAff = AppSettings.Link;
            if (strPreferredLang.ToLower() == "th-th")
                strURLAff = AppSettings.Link_TH;

            TemplateHandler.Assign(ref strNoticeMail, "rooturl", Common.GetAffiliateUserRootUrl());
            TemplateHandler.Assign(ref strNoticeMail, "lang", strPreferredLang);
            TemplateHandler.Assign(ref strNoticeMail, "fname", strFName);
            //TemplateHandler.Assign(ref strNoticeMail, "lname", strLName);
            TemplateHandler.Assign(ref strNoticeMail, "mailcontact", AppSettings.MailContact);
            TemplateHandler.Assign(ref strNoticeMail, "amount", FormatHandler.FormatNumber2(Amt, "en-US"));
            TemplateHandler.Assign(ref strNoticeMail, "transid", intTransId.ToString());
            TemplateHandler.Assign(ref strNoticeMail, "csdemail", AppSettings.MailContact);
            TemplateHandler.Assign(ref strNoticeMail, "winlostdate", dtmWLDate.ToString("MM/dd/yyyy"));
            string strSubject = TemplateHandler.ReplaceBlock(ref strNoticeMail, "subject", "");
            Common.SendEmail(strEmail, strSubject, strNoticeMail, AppSettings.BCCMail, AppSettings.CCMail);
        }

        private void ApprovedMail(Int64 intTransId, string strFName, string strLName, decimal Amt, DateTime dtmWLDate, string strEmail, string strPreferredLang, string strEmailSuffix)
        {
            string FileName = "Email/PaymentApp/PaymentAppEmail_" + strPreferredLang + strEmailSuffix + ".htm";

            string strNoticeMail = TemplateHandler.LoadTemplate(FileName).Replace("\r\n", "");
            string strURLAff = AppSettings.Link;
            if (strPreferredLang.ToLower() == "th-th")
                strURLAff = AppSettings.Link_TH;

            TemplateHandler.Assign(ref strNoticeMail, "rooturl", Common.GetAffiliateUserRootUrl());
            TemplateHandler.Assign(ref strNoticeMail, "lang", strPreferredLang);
            TemplateHandler.Assign(ref strNoticeMail, "fname", strFName);
            //TemplateHandler.Assign(ref strNoticeMail, "lname", strLName);
            TemplateHandler.Assign(ref strNoticeMail, "mailcontact", AppSettings.MailContact);
            TemplateHandler.Assign(ref strNoticeMail, "amount", FormatHandler.FormatNumber2(Amt, "en-US"));
            TemplateHandler.Assign(ref strNoticeMail, "transid", intTransId.ToString());
            TemplateHandler.Assign(ref strNoticeMail, "csdemail", AppSettings.MailContact);
            TemplateHandler.Assign(ref strNoticeMail, "winlostdate", dtmWLDate.ToString("MM/dd/yyyy"));
            string strSubject = TemplateHandler.ReplaceBlock(ref strNoticeMail, "subject", "");
            Common.SendEmail(strEmail, strSubject, strNoticeMail, AppSettings.BCCMail, AppSettings.CCMail);
        }

        public static string GetErrMsg(int intErrCode, ref string strErrObj)
        {
            switch (intErrCode)
            {
                case 1:
                    return Resources.Lang.Err1;

                case 2:
                    return Resources.Lang.Err2;

                case 3:
                    return Resources.Lang.Err3;

                case 4:
                    return Resources.Lang.Err4;

                case 5:
                    return Resources.Lang.Err5;

                case 101:
                    return Resources.Lang.Err101;

                case 102:
                    return Resources.Lang.Err102;

                case 103:
                    return Resources.Lang.Err103;

                case 104:
                    return Resources.Lang.Err104;

                case 105:
                    return Resources.Lang.Err105;

                case 107:
                    return Resources.Lang.Err107;

                case 111:
                    return Resources.Lang.Err111;

                case 112:
                    return Resources.Lang.Err112;

                case 113:
                    return Resources.Lang.Err113;

                case 114:
                    return Resources.Lang.Err114;

                case 115:
                    return Resources.Lang.Err115;

                case 116:
                case 500:
                    return Resources.Lang.Err116;

                case 100:
                    return Resources.Lang.Err100;

                case 999:
                    return Resources.Lang.Err999;

                case 1001:
                    return Resources.Lang.Err1001;

                case 1002:
                    strErrObj = "txtEmail";
                    return Resources.Lang.Err1002;

                case 1003:
                    strErrObj = "txtBet12UserName";
                    return Resources.Lang.Err1003;

                case 1004:
                    return Resources.Lang.Err1004;

                case 1005:
                    return Resources.Lang.Err1005;

                case 1006:
                    return Resources.Lang.Err1006;

                case 1007:
                    return Resources.Lang.Err1007;

                case 3006:
                    return Resources.Lang.Err3006;

                case 4001:
                    return Resources.Lang.Err4001;

                case 4002:
                    return Resources.Lang.Err4002;

                case 4003:
                    return Resources.Lang.Err4003;

                case 4004:
                    return Resources.Lang.Err4004;

                case 5001:
                    return Resources.Lang.Err5001;

                case 30010:
                    return Resources.Lang.Err30010;

                case 30011:
                    return Resources.Lang.Err30011;

                case 30012:
                    return Resources.Lang.Err30012;

                case 30014:
                    return Resources.Lang.Err30014;

                case 30015:
                    return Resources.Lang.Err30015;

                default:
                    return GetErrorText(intErrCode.ToString());
            }
        }

        public static string GetErrorText(string key)
        {
            key = "Err" + key;
            object text = HttpContext.GetGlobalResourceObject("Lang", key);

            return text == null ? key : text.ToString();
        }
    }
}