<%@ WebHandler Language="C#" Class="PaymentUpdate" %>

using System;
using System.Web.SessionState;
using System.Web;
using DNAide.Core;
using DNAide.Web;
using DNAide.Utils;

public class PaymentUpdate : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context = IBCHandler.InitializeCulture(context);
        try
        {
            if (!string.IsNullOrEmpty(context.Request["hdApprove"]))
            {
                string strActionType = context.Request["hdApprove"];
                string strAdminRemark = context.Request["txtAdminRemark"];
                string strAffRemark = context.Request["txtAffRemark"];
                string strPreferredLang = "E";
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
                        strMsgMail = ApprovedMail(intTransId, strFName, strLName, Amt, dtmWLDate, strEmail, strPreferredLang, strEmailSuffix);
                    else
                        strMsgMail = CancelMail(intTransId, strFName, strLName, Amt, dtmWLDate, strEmail, strPreferredLang, strEmailSuffix);
                    
                    //if(!string.IsNullOrEmpty(strMsgMail))
                    //    context.Response.Write("<script type='text/javascript'>parent.alert(\"" + strMsgMail + "\");</script>");
                    if (!string.IsNullOrEmpty(strMsgMail))
                        strMsg = strMsgMail;                    
                        context.Response.Write("<script type='text/javascript'>parent.UpdateSuccess('" + strMsg + "');</script>");
                }
                else
                {
                    string strErrObj = "error_item";
                    string strMsg = Common.GetErrMsg(intErrCode, ref strErrObj);
                    context.Response.Write("<script type='text/javascript'>parent.UpdateErr(\"" + strMsg + "\");</script>");
                }
            }
        }
        catch (Exception ex)
        {
            context.Response.Write("<script type='text/javascript'>parent.UpdateErr(\"" + ex.Message + "\");</script>");
            DNAideLog.Write(ex.Message);
        }
    }
    
    #region Email Content of Request Payment
    string CancelMail(Int64 intTransId, string strFName, string strLName, decimal Amt, DateTime dtmWLDate, string strEmail, string strPreferredLang, string strEmailSuffix)
    {
        try
        {
            string FileName = "Email/PaymentCan/PaymentCanEmail_" + strPreferredLang + strEmailSuffix + ".htm";            
            string strNoticeMail = TemplateHandler.LoadTemplate(FileName).Replace("\r\n", "");
            string strURLAff = AppSettings.Link;
            if (strPreferredLang.ToLower() == "th-th")
                strURLAff = AppSettings.Link_TH;

            TemplateHandler.Assign(ref strNoticeMail, "rooturl", AppSettings.UrlAffSite);
            TemplateHandler.Assign(ref strNoticeMail, "lang", strPreferredLang);
            TemplateHandler.Assign(ref strNoticeMail, "fname", strFName);
            //TemplateHandler.Assign(ref strNoticeMail, "lname", strLName);
            TemplateHandler.Assign(ref strNoticeMail, "mailcontact", AppSettings.MailContact);
            TemplateHandler.Assign(ref strNoticeMail, "amount", FormatHandler.FormatNumber2(Amt, "en-US"));
            TemplateHandler.Assign(ref strNoticeMail, "transid", intTransId.ToString());
            TemplateHandler.Assign(ref strNoticeMail, "csdemail", AppSettings.MailContact);
            TemplateHandler.Assign(ref strNoticeMail, "winlostdate", dtmWLDate.ToString("MM/dd/yyyy"));
            string strSubject = TemplateHandler.ReplaceBlock(ref strNoticeMail, "subject", "");            
            SendMailService.SendMail2(AppSettings.MailServer, strEmail, AppSettings.FromMail, AppSettings.CCMail, AppSettings.BCCMail, strSubject, strNoticeMail, AppSettings.MailPwd);
            
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        return "";
    }
    #endregion

    #region Email Content of Request Payment
    string ApprovedMail(Int64 intTransId, string strFName, string strLName, decimal Amt, DateTime dtmWLDate, string strEmail, string strPreferredLang, string strEmailSuffix)
    {
        try
        {
            string FileName = "Email/PaymentApp/PaymentAppEmail_" + strPreferredLang + strEmailSuffix + ".htm";            
          
            string strNoticeMail = TemplateHandler.LoadTemplate(FileName).Replace("\r\n", "");
            string strURLAff = AppSettings.Link;
            if (strPreferredLang.ToLower() == "th-th")
                strURLAff = AppSettings.Link_TH;

            TemplateHandler.Assign(ref strNoticeMail, "rooturl", AppSettings.UrlAffSite);
            TemplateHandler.Assign(ref strNoticeMail, "lang", strPreferredLang);
            TemplateHandler.Assign(ref strNoticeMail, "fname", strFName);
            //TemplateHandler.Assign(ref strNoticeMail, "lname", strLName);
            TemplateHandler.Assign(ref strNoticeMail, "mailcontact", AppSettings.MailContact);
            TemplateHandler.Assign(ref strNoticeMail, "amount", FormatHandler.FormatNumber2(Amt, "en-US"));
            TemplateHandler.Assign(ref strNoticeMail, "transid", intTransId.ToString());
            TemplateHandler.Assign(ref strNoticeMail, "csdemail", AppSettings.MailContact);
            TemplateHandler.Assign(ref strNoticeMail, "winlostdate", dtmWLDate.ToString("MM/dd/yyyy"));
            string strSubject = TemplateHandler.ReplaceBlock(ref strNoticeMail, "subject", "");           
            SendMailService.SendMail2(AppSettings.MailServer, strEmail, AppSettings.FromMail, AppSettings.CCMail, AppSettings.BCCMail, strSubject, strNoticeMail, AppSettings.MailPwd);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        return "";
    }
    #endregion
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}
